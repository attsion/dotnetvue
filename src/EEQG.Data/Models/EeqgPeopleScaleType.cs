using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EEQG.Data.Models
{
    public class EeqgPeopleScaleType
    {
        #region 静态成员
        public static readonly string DirPath = AppDomain.CurrentDomain.BaseDirectory + @"data\ScaleType\";
        public static List<EeqgPeopleScaleType> Types { get; set; }
        public static IEnumerable<string> TypesNames
        {
            get
            {
                return Types.Select(x => x.Name);
            }
        }
        static EeqgPeopleScaleType()
        {
            Load();
        }
        protected static List<EeqgPeopleScaleType> Load()
        {
            var tys = new List<EeqgPeopleScaleType>();
            try
            {
                var files = Directory.GetFiles(DirPath, "*.conf");
                for (int i = 0; i < files.Length; i++)
                {
                    var f = files[i];
                    EeqgPeopleScaleType t = new EeqgPeopleScaleType();
                    XElement root = XElement.Load(f);
                    t.Name = root.Attribute("name").Value;
                    t.Introduce = root.Attribute("introduce").Value;
                    t.Version = root.Attribute("version").Value;
                    //读取option
                    var xops = root.Element("head").Elements("option");
                    var ops = new List<EeqgPeopleScaleTypeItemOption>();
                    foreach (var xo in xops)
                    {
                        var op = new EeqgPeopleScaleTypeItemOption();
                        op.Name = xo.Attribute("name").Value;
                        op.Default = xo.Attribute("default").Value;
                        op.Options = xo.Attributes().Where(x => x.Name.LocalName.StartsWith("op")).Select(x => x.Value).ToList();
                        op.MutiSelect = bool.Parse(xo.Attribute("mutiSelect").Value);
                        ops.Add(op);
                    }
                    //读取group
                    var xgro = root.Element("head").Elements("group");
                    var gros = new List<EeqgPeopleScaleGroup>();
                    foreach (var xo in xgro)
                    {
                        var op = new EeqgPeopleScaleGroup();
                        op.Name = xo.Attribute("name").Value;
                        op.Text = xo.Attribute("text").Value;
                        op.Index = int.Parse(xo.Attribute("index").Value);
                        gros.Add(op);
                    }

                    //读取item
                    var xitems = root.Element("body").Elements("item");
                    var items = new List<EeqgPeopleScaleTypeItem>();
                    foreach (var xit in xitems)
                    {
                        var it = new EeqgPeopleScaleTypeItem();
                        it.Title = xit.Attribute("title").Value;
                        it.Group = xit.Attribute("group").Value == "" ? null : gros.First(x => x.Name == xit.Attribute("group").Value);
                        it.Index = xit.Attribute("index").Value;
                        it.Option = xit.Attribute("option").Value == "" ? ops.First() : ops.First(x => x.Name == xit.Attribute("option").Value);
                        it.Default = xit.Attribute("default").Value == "" ? it.Option.Default : xit.Attribute("default").Value;
                        items.Add(it);
                    }
                    t.Items = items;
                    //加入集合
                    tys.Add(t);
                }

                Types = tys;
                return Types;
            }
            catch (Exception ee)
            {
                throw (new Exception("量表设置文件读取错误" + ee.Message));
            }

        }
        #endregion

        public string Name { get; set; }
        public string Introduce { get; set; }
        public string Version { get; set; }
        public List<EeqgPeopleScaleTypeItem> Items { get; set; }
        public bool GroupMode
        {
            get
            {
                var gs = Items.GroupBy(x => x.Group);
                return !(gs.Count() == 1 && gs.First().Key == null);
            }

        }
        public EeqgPeopleScaleType()
        {
            Items = new List<EeqgPeopleScaleTypeItem>();
        }
    }

    public class EeqgPeopleScaleTypeItem
    {
        public string Title { get; set; }
        public string Index { get; set; }
        public EeqgPeopleScaleGroup Group { get; set; }
        public string Default { get; set; }
        public EeqgPeopleScaleTypeItemOption Option { get; set; }
        //public EeqgPeopleScaleTypeItem()
        //{
        //    Option = new EeqgPeopleScaleTypeItemOption();
        //}
    }
    public class EeqgPeopleScaleGroup
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
    }
    public class EeqgPeopleScaleTypeItemOption
    {
        public string Name { get; set; }
        public string Default { get; set; }
        public List<string> Options { get; set; }
        public bool MutiSelect { get; set; }
        public EeqgPeopleScaleTypeItemOption()
        {
            Options = new List<string>();
        }
    }

    public class EeqgPeopleScaleResult
    {
        public static EeqgPeopleScaleResult Build(EeqgPeopleScaleType type, EeqgPeopleScale values = null, EeqgPeople people = null)
        {
            var valueIsNull = values == null;
            if (!valueIsNull)
            {
                if (type.Name != values.ScaleType)
                {
                    throw (new Exception("传入的量表类型和量表值不一致"));
                }
                if (type.Items.Count != values.ValueList.Count)
                {
                    throw (new Exception("传入的量表类型和量表值数目不一致"));
                }
            }


            var m = new EeqgPeopleScaleResult();
            m.ScaleType = type.Name;
            m.Version = type.Version;
            m.Introduce = type.Introduce;


            m.People = people;

            m.Items = new List<EeqgPeopleScaleResultItem>();


            var getItem = new Func<EeqgPeopleScaleTypeItem, string, EeqgPeopleScaleResultItem>((t, v) => {
                var item = new EeqgPeopleScaleResultItem();
                item.Title = t.Title;
                item.Index = t.Index;
                item.Group = t.Group;
                item.Default = t.Default;
                item.Option = t.Option;
                item.Value = v;
                return item;
            });
            if (!type.GroupMode)
            {
                for (int i = 0; i < type.Items.Count; i++)
                {
                    m.Items.Add(getItem(type.Items[i], type.Items[i].Default));
                }
            }
            else
            {
                var gs = type.Items.GroupBy(x => x.Group);
                var nullgrp = gs.Where(x => x.Key == null);
                var gts = gs.Where(x => x.Key != null).OrderBy(x => x.Key.Index);
                var numitem = 0;
                foreach (var g in gts)
                {
                    foreach (var item in g)
                    {
                        m.Items.Add(getItem(item, item.Default));
                        numitem++;
                    }
                }
                if (nullgrp.Count() > 0)
                {
                    var g = nullgrp.First();
                    foreach (var item in g)
                    {
                        m.Items.Add(getItem(item, item.Default));
                        numitem++;
                    }
                }
            }
            if (!valueIsNull)
            {
                m.SetScaleData(values);
            }

            return m;
        }


        public EeqgPeople People { get; set; }
        public string ID { set; get; }
        public DateTime? CreatTime { set; get; }
        public String Num { set; get; }

        public string ScaleType { set; get; }
        public string Introduce { get; set; }
        public string Version { get; set; }
        public List<EeqgPeopleScaleResultItem> Items;
        protected EeqgPeopleScale setValue;

        public void SetScaleData(EeqgPeopleScale values)
        {
            setValue = values;
            this.ID = values.ID;
            this.CreatTime = values.CreatTime;
            this.Num = values.Num;

            for (int i = 0; i < values.ValueList.Count; i++)
            {
                Items[i].Value = values.ValueList[i];
            }
        }
        public EeqgPeopleScale GetScaleData()
        {
            List<string> ls = new List<string>();
            foreach (var item in Items)
            {
                ls.Add(item.Value);
            }
            if (setValue != null)
            {
                setValue.ValueList = ls;
                return setValue;
            }
            else
            {
                var value = new EeqgPeopleScale();
                value.ID = ID;
                value.CreatTime = CreatTime;
                value.Num = Num;
                value.ScaleType = ScaleType;
                value.EeqgPeople = null;
                value.ValueList = ls;
                return value;
            }

        }

        public bool GroupMode
        {
            get
            {
                var gs = Items.GroupBy(x => x.Group);
                return !(gs.Count() == 1 && gs.First().Key == null);
            }

        }

    }
    public class EeqgPeopleScaleResultItem : EeqgPeopleScaleTypeItem
    {
        public string Value { get; set; }
    }
}
