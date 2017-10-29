<template>
    <Menu active-name="1-2" theme="light" width="auto" :open-names="['1']" @on-select="itemSelected">
                   
                    <Submenu name="1">
                        <template slot="title">
                            <Icon type="ios-navigate" :size="iconSize"></Icon>
                            导航一1
                        </template>
                        <MenuItem name="1-1">选项 1</MenuItem>
                        <MenuItem name="1-2">选项 2</MenuItem>
                        <MenuItem name="1-3">选项 3</MenuItem>
                    </Submenu>
                    <Submenu name="2">
                        <template slot="title">
                            <Icon type="ios-keypad" :size="iconSize"></Icon>
                            导航二
                        </template>
                        <MenuItem name="2-1">选项 1</MenuItem>
                        <MenuItem name="2-2">选项 2</MenuItem>
                    </Submenu>
                    <Submenu name="3">
                        <template slot="title">
                            <Icon type="ios-analytics" :size="iconSize"></Icon>
                            示例
                        </template>
                        <MenuItem name="welcome">欢迎</MenuItem>
                        <MenuItem name="counter">计数器</MenuItem>
                        <MenuItem name="fetchdata">数据</MenuItem>
                    </Submenu>
                </Menu>

</template>
<script>
    export default {
        name:"menu-component",
        props:["iconSize"],
        methods:{
            itemSelected(name){
                this.$router.push(name);
            },
        },
    }
</script>


