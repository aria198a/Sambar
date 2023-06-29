export const state = {
    user: JSON.parse(sessionStorage.getItem("user")),
    isLogin: sessionStorage.getItem("islogin") //false
};
export const actions = {};

export const mutations = {
    setUser(state, payload) {
        sessionStorage.setItem("user", JSON.stringify(payload));
        localStorage.setItem("user", JSON.stringify(payload));
        state.user = payload;
        state.isLogin = true;
        sessionStorage.setItem("islogin", true);
        localStorage.setItem("islogin", true);
    },
    removeUser() {
        sessionStorage.removeItem("user");
        localStorage.removeItem("user");
        state.isLogin = false;
        sessionStorage.removeItem("islogin");
        localStorage.removeItem("islogin");
        sessionStorage.removeItem("ConSub");
        sessionStorage.removeItem("TokenID");
        
        sessionStorage.removeItem("userType");
        sessionStorage.removeItem("isAgree");

        sessionStorage.removeItem("planDetail");
    },
};
export const getters = {
    isAuthenticated: () =>  !!sessionStorage.getItem("user"),
};

export default {
    state,
    actions,
    mutations,
    getters,
    namespaced: true,
};
