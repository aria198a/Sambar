import { createStore } from "vuex";
import auth from "./auth";
export default createStore({
    state: {},
    mutations: {
        testConsol() {
            console.log('word')
        }
    },
    actions: {},
    modules: { auth },
});