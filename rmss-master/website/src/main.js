import { createApp } from 'vue'
import store from './store';
import './style.css'
import App from './App.vue'
import router from './router';
import Loading from 'vue-loading-overlay';

const app=createApp(App)
app.use(store)
app.use(router)
app.mount('#app')
app.component("Loading", Loading);

router.beforeEach((to, from, next) => {
    const isLogin = store.state.auth.isLogin;
    console.log("islogin:"+isLogin);
    if (isLogin) {
        if(to.path == '/login') {
            alert('已登入')
            next('/');
            return
        }

        if(to.path != '/agree' && sessionStorage.getItem("isAgree") == 0 && sessionStorage.getItem("userType") == 0)  {
            next('/agree');
            return
        } 
    } else {
        
        //if( to.path !== '/login'){
        //    alert('請登入')
          //  next('/login')
            //return
       // }
    }
    next();
})
