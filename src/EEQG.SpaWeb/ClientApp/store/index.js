import Vue from 'vue'
import Vuex from 'vuex'
//import * as actions from './actions'
//import * as getters from './getters'
//import cart from './modules/cart'
//import products from './modules/products'
//import createLogger from '../../../src/plugins/logger'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
    state: {
        myValue:"aa",
    },
    getters: {
        getValue(state) {
            return state.myValue;
        },
    },
    actions: {
        setValue({ commit }, nv) {
           return new promise(()=>{

        });
        },
    },

    mutations: {
        changeValue(state, { text}){
            state.myValue=text;
        },
    },
    
    


  //actions,
  //getters,
  //modules: {
  //  cart,
  //  products
  //},
  //strict: debug,
  ////plugins: debug ? [createLogger()] : []
})
