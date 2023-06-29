<template>
    <div class="login">
        <div class="max-w-xl space-y-8">
            <img src="../assets/image/logo.svg" class="mx-auto" alt="個資權利管理系統">
            <Transition name="fade">
                <div v-if="form.userType == 0">
                    <h1 class="step-title">RMS的目的</h1>
                    <p>個資權利管理系統（Rights Management System，以下簡稱RMS），是以使用者（即個資當事人）之意願為核心，進行與使用者資料蒐集、處理、利用以及後續再利用相關的動態審查與追蹤。透過網路，使用者能即時掌握自身資料動態並行使有關的一切權利。</p>
                    <p>有別於過往僅給予個資當事人「同意」或「不同意」兩種選擇，RMS採用的偏好選項表單有詳細的目的與科別分類。當有研究計畫需要再次利用您的資料時，RMS會根據您預先設定的偏好拒絕、核准其使用，或另行通知您取得同意，使個資當事人能更細緻且精確地決定自己的資料如何被再利用。</p>
                </div>
            </Transition>
            <form class="login-form">
                <div class="form-control">
                    <select class="select w-full" v-model="form.userType">
                        <option value="0">個資當事人</option>
                        <option value="1">資料利用者</option>
                    </select>
                </div>
                <div class="form-control">
                    <input type="text" placeholder="請輸入帳號" class="input input-bordered w-full" v-model="loginData.Id" required />
                </div>
                <div class="form-control">
                    <input type="password" placeholder="請輸入密碼" class="input input-bordered w-full" v-model="loginData.DecryptionPass" required />
                </div>
                <div class="form-control">
                    <input type="text" placeholder="請輸入圖形驗證碼" class="input input-bordered w-full" v-model="loginData.CodePass" required />

                </div>
                <div  style="vertical-align:central; margin-top:3px">
                    <img :src="imageCode" class="  mr-3 " style="width:100px;height:35px; display:inline" />
                    <input type="button" class="btn btn-sm " style=" display: inline" @click="getImageCode" value="重新產生" />
                </div>

                <button type="button" class="btn btn-block" @click="handleSetProfile">確定</button>
            </form>
        </div>
    </div>
</template>
<script>
    import { defineComponent, computed, reactive, onMounted, ref } from 'vue';
    import { useRouter } from 'vue-router'
    import { useStore } from "vuex"
    import { apihandler } from '../api/apiHandler.js';
    export default defineComponent({
        setup() {
            const router = useRouter()
            const store = useStore();
            const apiHander = apihandler();
            const loginData = reactive({
                Id: "",
                DecryptionPass: "",
                CodePass: "",
                DecryptionCodePass: "",
                Type: "login",
                TokenID: "",
                Token: "",
                Page: "",
            });
            const imageCode = ref("");
            const form = reactive({
                username: null,
                userType: 0,
            })
            const states = reactive({
                username: computed(() => store.state.auth.user.username),
                isLogin: computed(() => store.state.auth.isLogin),
            });
            const reqData = reactive({
                TokenID: "",
                Token: "",
                Page: "",
            });

            //// ------ fun 事件-------------////
            const handleSetProfile = () => {
                if (loginData.Id == '') {
                    alert('帳號未填');
                    return
                }
                if (loginData.DecryptionPass == '') {
                    alert('密碼未填');
                    return
                }
                if (loginData.CodePass == '') {
                    alert('圖形驗證碼未填');
                    return
                }
                form.username = loginData.Id;
                apiHander.FunctionToken(login, loginData);


            }

            //// ------------api --------------////
            const apiLogin = (data) => apiHander.userRequest.post("api/User/Login", data);
            const apiGetImageCode = (data) => apiHander.userRequest.post("api/User/GetImageCode", data);
            const apiCheckPrivated = (data) => apiHander.userRequest.post("api/PrivacyPre/CheckPrivated", data);

            // 取得 imageCode
            const getImageCode = () => {
                apiGetImageCode()
                    .then((res) => {
                        const json = res.data;
                        //console.log(json.Status)
                        if (json.Status) {
                            imageCode.value = json.Data.Image;
                            loginData.DecryptionCodePass = json.Data.Code;
                        }
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }

            // 登入
            const login = (data) => {
                apiLogin(data)
                    .then((res) => {                 
                        const json = res.data;
                        if (json.Status) {
                            const jsonToken = JSON.parse(json.Data);
                            sessionStorage.setItem("TokenID", jsonToken);
                            sessionStorage.setItem("Version", true);
                            store.commit("auth/setUser", form);

                            if (form.userType == 0) {
                                apiHander.FunctionToken(checkPrivated, reqData);
                            } else if (form.userType == 1) { //資料利用，至計畫總覽
                                router.push({
                                    path: '/v2'
                                });
                            }
                        } else {
                            alert(json.Data);
                        }
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }

            // 檢查是否有提交過
            const checkPrivated = (data) => {
                apiCheckPrivated(data)
                    .then((res) => {
                        const json = res.data;
                        if (json.Status) {
                            let isPried = JSON.parse(json.Data).IsPried;
                            if (isPried) {
                                router.push({ path: "/v2" });
                            }
                            else {
                                router.push({
                                    path: 'agree/v2'
                                });
                            }
                        }
                        else {
                            alert(json.Data);
                        }
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }


          

            ////------------------------------////
            onMounted(() => {
                getImageCode();
            })
            return {
                states,
                handleSetProfile,
                form,
                loginData,
                imageCode,
                getImageCode
            }
        }
    })
</script>
<style lang="postcss" scoped>
    .login {
        background-image: url('../assets/image/bg-login.png');
        @apply flex justify-center items-center min-h-screen bg-primary-500 text-lg text-justify p-6 sm:p-0;
    }

    .login-form {
        @apply max-w-xs space-y-4 mx-auto;
    }

    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.5s ease;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }
</style>