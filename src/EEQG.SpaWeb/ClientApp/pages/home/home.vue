<template>
    <div>
       首页{{v}}
        <Button @click="changeValue">点击修改</Button>
    </div>
</template>

<script type="text/javascript">
    import { Button } from 'iview';
    export default {
        computed:{
            v(){
                return this.$store.state.myValue;
            },
        },
        methods:{
            changeValue() {
                this.$store.commit("changeValue", {text:"哈哈"});
            },
        }
    }
</script>