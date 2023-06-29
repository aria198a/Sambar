<template>
    <div class="agree">
        <div class="max-w-xl space-y-8">
            <h1 class="step-title">
                數位遺產設定
            </h1>
            <div class="space-y-4 bg-white p-8 border rounded-xl ">
                <p>您可透過以下選項，像預立醫療決定一樣，預先決定您的資料，在您身故後將何去何從（範圍：僅限有彙整到RMS的資料權利）</p>
                <ul>
                    <li>
                         <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                            <input type="radio" :name="'radio-1'"  :value="0" v-model="localData.details.IH_TYPE"/>
                            <span class="inline-block align-top ml-1">全數捐贈使用</span>
                        </div>
                    </li>
                    <li>
                        <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                            <input type="radio" :name="'radio-1'"  :value="1" v-model="localData.details.IH_TYPE" />
                            <span class="inline-block align-top ml-1">循生前之資料利用偏好設定，決定身故後資料之處理方式</span>
                        </div>
                    </li>
                    <li>
                        <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                            <input type="radio" :name="'radio-1'" :value="2" v-model="localData.details.IH_TYPE" />
                            <span class="inline-block align-top ml-1">授權我的繼承人或醫療代理人（擇一）於身故後代為決定</span>
                             <div v-if="localData.details.IH_TYPE == 2" style="margin: 0.5rem;" class="grid grid-rows-3 grid-cols-12">
                                <div class="sm:col-span-12  row-span-1 col-span-12 radioBlock px-3 py-1">
                                    <input type="radio" :name="'radio-2'" :value="0" v-model="localData.type"/>
                                    <span class="inline-block align-top ml-1">繼承人資訊</span>
                                </div>
                                <input style="margin-right: 0.25rem;" class="sm:col-span-4 row-span-1 col-span-12 input input-bordered mb-1" 
                                        type="text"
                                        placeholder="姓名" 
                                         v-model="localData.details.IH_HEIR"
                                        />
                                 <input
                                        class="sm:col-span-8 row-span-1 col-span-12 input input-bordered mb-1" 
                                        type="text" 
                                        placeholder="聯絡資訊"
                                         v-model="localData.details.IH_HEIR_DESCRIPTION"
                                        />
                                <div class="sm:col-span-12  row-span-1 col-span-12 radioBlock px-3 py-1">
                                    <input type="radio" :name="'radio-2'"  :value="1"  v-model="localData.type"/>
                                    <span class="inline-block align-top ml-1">醫療代理人資訊</span>
                                </div>
                                <input  style="margin-right: 0.25rem;" class="sm:col-span-4 row-span-1 col-span-12 input input-bordered mb-1" 
                                        type="text"
                                        placeholder="姓名"
                                         v-model="localData.details.IH_AGENT"
                                        />
                                 <input
                                        class="sm:col-span-8 row-span-1 col-span-12 input input-bordered mb-1" 
                                        type="text" 
                                        placeholder="聯絡資訊"
                                         v-model="localData.details.IH_AGENT_DESCRIPTION"
                                        />
                             </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="text-center">
                <button v-if="localData.status == 0" style="margin: 0.25rem;" class="btn w-40" @click="addInheritanceFunction">新增</button>
                <button v-if="localData.status == 1" style="margin: 0.25rem;" class="btn w-40" @click="updateInheritanceFunction">修改</button>
                <button style="margin: 0.25rem;" class="btn w-40" @click="router.go(-1)">返回</button>
            </div>
        </div>
    </div>
</template>
<script>
    import { defineComponent, ref,reactive ,onMounted} from 'vue';
    import { useStore } from "vuex";
    import { useRouter } from 'vue-router'
    import { apihandler } from '../../api/apiHandler.js';
export default defineComponent({
    setup() {
        const store = useStore();
        const apiHander = apihandler();
        const router = useRouter()
        const localData = reactive({
            details:{
                IH_TYPE:null,
                IH_HEIR:null,
                IH_HEIR_DESCRIPTION:null,
                IH_AGENT:null,
                IH_AGENT_DESCRIPTION:null
            },
            type:null,
            status:0,
        });

        //取數位遺產資訊
        const apiGetInheritance = (data) => apiHander.userRequest.post("api/PrivacyPre/GetInheritance", data);
        const getInheritance = (data) => {
            apiGetInheritance(data)
                .then((res) => {
                    const json = res.data;
                    if(json.Data == "null"){
                        localData.status = 0
                    }else{
                        localData.status = 1
                        var jsData = JSON.parse(json.Data)
                        localData.details = jsData
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        };

        //新增數位遺產資訊
        const apiAddInheritance = (data) => apiHander.userRequest.post("api/PrivacyPre/AddInheritance", data);
        const addInheritance = (data) => {
            apiAddInheritance(data)
                .then((res) => {
                    const json = res.data;
                    if(json.Status){
                        
                        apiHander.FunctionToken(getInheritance, {});
                        alert("新增成功")
                    }else{
                        alert("新增失敗")
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        };
        const addInheritanceFunction = () =>{
            if (confirm("確定要新增？") == false) {
                return;
            }

            var msg = ""
            if(localData.details.IH_TYPE == null)
                msg += "請選擇方法\n"

            if(localData.details.IH_TYPE == 2){
                if(localData.type == null){
                    msg += "請選擇繼承人或代理人\n"
                }else if(localData.type == 0 && localData.details.IH_HEIR == null && localData.details.IH_HEIR_DESCRIPTION == null){
                    msg += "請填寫繼承人資訊\n"
                }else if(localData.type == 1 && localData.details.IH_AGENT == null && localData.details.IH_AGENT_DESCRIPTION == null){
                    msg += "請填寫代理人資訊\n"
                }
            }
            
            if(localData.details.IH_TYPE == 2 && localData.type ==0){
                localData.details.IH_AGENT = null
                localData.details.IH_AGENT_DESCRIPTION = null
            }else if(localData.details.IH_TYPE == 2 && localData.type ==1){
                localData.details.IH_HEIR = null
                localData.details.IH_HEIR_DESCRIPTION = null
            }


            if(msg == ""){
                apiHander.FunctionToken(addInheritance, localData.details);
            }else{
                alert(msg)
            }

        }

        //修改數位遺產資訊
        const apiUpdateInheritance = (data) => apiHander.userRequest.post("api/PrivacyPre/UpdateInheritance", data);
        const updateInheritance = (data) => {
            apiUpdateInheritance(data)
                .then((res) => {
                    const json = res.data;
                    if(json.Status){
                        
                        apiHander.FunctionToken(getInheritance, {});
                        alert("修改成功")
                    }else{
                        alert("修改失敗")
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        };
        const updateInheritanceFunction = () =>{
            if (confirm("確定要修改？") == false) {
                return;
            }

            var msg = ""
            if(localData.details.IH_TYPE == null)
                msg += "請選擇方法\n"

            if(localData.details.IH_TYPE == 2){
                if(localData.type == null){
                    msg += "請選擇繼承人或代理人\n"
                }else if(localData.type == 0 && localData.details.IH_HEIR == null && localData.details.IH_HEIR_DESCRIPTION == null){
                    msg += "請填寫繼承人資訊\n"
                }else if(localData.type == 1 && localData.details.IH_AGENT == null && localData.details.IH_AGENT_DESCRIPTION == null){
                    msg += "請填寫代理人資訊\n"
                }
            }

            if(localData.details.IH_TYPE == 2 && localData.type ==0){
                localData.details.IH_AGENT = null
                localData.details.IH_AGENT_DESCRIPTION = null
            }else if(localData.details.IH_TYPE == 2 && localData.type ==1){
                localData.details.IH_HEIR = null
                localData.details.IH_HEIR_DESCRIPTION = null
            }

            if(msg == ""){
                apiHander.FunctionToken(updateInheritance, localData.details);
            }else{
                alert(msg)
            }

        }


        ///=== 初始
        onMounted(() => {
            apiHander._sessionData("planDetail", "");
            if (store.state.auth.user === null) {
                alert("請登入");
                router.push({ path: "/Login" });
            }
            else {
                apiHander.FunctionToken(getInheritance, {});
            }
        });

        
        
        return {
            router,
            localData,
            getInheritance,
            addInheritanceFunction,
            updateInheritanceFunction
        }
    }
})
</script>
<style lang="postcss" scoped>
    .agree {
        @apply flex justify-center min-h-screen text-justify p-6 sm:p-0;
    }
</style>