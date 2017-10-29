import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import app from './app.vue';
import iView from 'iview';
import 'iview/dist/styles/iview.css';    // 使用 CSS

import welcome from './pages/welcome/welcome.vue'
import home from './pages/home/home.vue';
import counter from './pages/counter/counter.vue';
import fetchdata from './pages/fetchdata/fetchdata.vue';
import store from './store';

Vue.use(VueRouter);
Vue.use(iView);




const routes = [
    { path: '/', component: home },
    { path: '/welcome', component: welcome },
    { path: '/counter', component: counter },
    { path: '/fetchdata', component: fetchdata }
];

new Vue({
    el: '#app-root',
    store,
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(app)
});
