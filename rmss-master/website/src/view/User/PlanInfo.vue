<template>
    <div class="container mx-auto px-6 pb-12 sm:px-0">
        <div class="text-sm breadcrumbs">
            <ul>
                <li><a @click="toHome">RMS</a></li> 
                <li><a @click="toHome">增修計畫</a></li> 
                <li>計畫基本資料</li>
            </ul>
        </div>
        <h1 class="step-title">計畫基本資料</h1>
        <div class="logon">
            <div class="sm:w-9/12 w-full space-x-4">
                <div class="card bg-base-100 mb-6 shadow">
                    <div class="card-body" >
                        <span class="text-center"><span class="text-error">*</span> 為必填欄位</span>
                        <div class="grid grid-rows-2 grid-cols-12">
                            <template v-for="(item, index) in planText" :key="index">
                                <p v-if="item.inputText.length == 1 && item.title != '研究目的'" class="sm:col-span-3 row-span-1 col-span-12 flex items-center pr-3">
                                    <span class='text-error'>* </span>
                                    {{ item.title }}
                                </p>
                                <p v-else class="sm:col-span-3 row-span-2 col-span-12 flex items-center">
                                    <span v-if="item.title != '協同主持人'" class='text-error'>* </span>
                                    {{ item.title }}
                                    <span v-if="item.title == '研究目的'">({{ planDetail.Main.PURPOSE ? planDetail.Main.PURPOSE.length : 0 }}/250)</span>
                                </p>
                                <template v-for="(item2, index2) in item.inputText" :key="index2">
                                    <input
                                        v-if="item.title !== '研究目的'" 
                                        class="sm:col-span-9 row-span-1 col-span-12 input input-bordered mb-1" 
                                        type="text" 
                                        :placeholder=item2.placeholder 
                                        v-model="planDetail.Main[item2.key]"
                                    />
                                    <textarea
                                        v-else 
                                        class="sm:col-span-9 row-span-2 col-span-12 textarea textarea-bordered text-base"
                                        maxlength="250"
                                        rows="3"
                                        :placeholder=item2.placeholder
                                        v-model="planDetail.Main[item2.key]"
                                    ></textarea>
                                </template>

                                <p  class="sm:col-span-3 row-span-1 col-span-12 flex items-center pr-3"></p>
                                <p style="height: 1.5rem" class="sm:col-span-9 row-span-1 col-span-12  mb-1"></p>
                                
                            </template>
                        </div>
                    </div>
                </div>
                <div class="text-center space-x-2">
                    <router-link class="btn w-40 btn-outline" to="/" @click="removeDetail">取消</router-link>
                    <!--<router-link class="btn w-40" to="/planContext" @click="saveDetail()">下一步</router-link>-->
                  <button class="btn w-40" @click="saveDetail()">下一步</button>
                </div>
              <ul class="steps grid mt-12">
                <li class="step step-primary">計畫基本資料</li>
                <li class="step ">再利用脈絡</li>
                <li class="step ">再利用類型</li>
              </ul>
            </div>
        </div>
    </div>
  
</template>
<script>
    import { defineComponent, computed, reactive, ref, onMounted } from 'vue';
    import { useStore } from "vuex";
    import { useRouter } from 'vue-router';
    import { useRoute } from 'vue-router';
    import { apihandler } from '../../api/apiHandler.js';
    export default defineComponent({
        setup() {
            const router = useRouter();
            const apiHander = apihandler();
            const route = useRoute();

            //=== 事件
            const saveDetail = function () {
                var planDetailCheck = ["NAME", "ENGLISHNAME", "RESEARCHINSTITUTE", "REQUESTER", "FUNDING", "HOSTNAME", "HOSTJOBTITLE", "VICEHOSTNAME", "VICEHOSTJOBTITLE", "CONTACTPERSON", "CONTACTNUMBER", "PURPOSE"]
                var checkValue = true
                planDetailCheck.forEach( (val, idx) => {
                    if (planDetail.value.Main[val] == null || planDetail.value.Main[val] == "") {
                        if ( idx == 1 || idx == 7 || idx == 8 ) {
                            planDetail.value.Main[val] = ""
                        }else
                            checkValue = false;
                    }
                })
                if (checkValue) {
                    apiHander._sessionData("planDetail", JSON.stringify(planDetail.value))
                    router.push({path: '/planContext'});
                } else {
                    alert('尚有資料未填! 請確認填寫後至下一頁');
                }
            };
            const removeDetail = function () {
                apiHander._sessionData("planDetail", "");
            };
            const toHome = ()=>{
              let confirm = window.confirm("資料尚未儲存，是否確認離開？");
              if(confirm)
                router.push({path:'/'});
            };
            //===== model
            const planText = reactive([
                {
                    title: "計畫名稱",
                    inputText: [
                        {
                            placeholder: "中文",
                            key: "NAME"
                        },
                        {
                            placeholder: "英文(選填，請依研究狀況填寫)",
                            key: "ENGLISHNAME"
                        }
                    ]
                },
                {
                    title: "研究機構/執行單位(院校/科部系所)",
                    inputText: [
                        {
                            placeholder: "",
                            key: "RESEARCHINSTITUTE"
                        }
                    ]
                },
                {
                    title: "委託單位/藥廠",
                    inputText: [
                        {
                            placeholder: "中文",
                            key: "REQUESTER"
                        }
                    ]
                },
                {
                    title: "經費來源",
                    inputText: [
                        {
                            placeholder: "",
                            key: "FUNDING"
                        }
                    ]
                },
                {
                    title: "主要主持人",
                    inputText: [
                        {
                            placeholder: "姓名",
                            key: "HOSTNAME"
                        },
                        {
                            placeholder: "職稱",
                            key: "HOSTJOBTITLE"
                        }
                    ]
                },
                {
                    title: "協同主持人",
                    inputText: [
                        {
                            placeholder: "姓名",
                            key: "VICEHOSTNAME"
                        },
                        {
                            placeholder: "職稱",
                            key: "VICEHOSTJOBTITLE"
                        }
                    ]
                },
                {
                    title: "聯絡人",
                    inputText: [
                        {
                            placeholder: "姓名",
                            key: "CONTACTPERSON"
                        }
                    ]
                },
                {
                    title: "上班時間聯絡電話",
                    inputText: [
                        {
                            placeholder: "",
                            key: "CONTACTNUMBER"
                        }
                    ]
                },
                {
                    title: "研究目的",
                    inputText: [
                        {
                            placeholder: "請簡要說明研究目的(250字內)",
                            key: "PURPOSE"
                        }
                    ]
                },
            ]);
            const planDetail = ref({
                Main: {
                    NAME: "",
                    ENGLISHNAME: "",
                    RESEARCHINSTITUTE: "",
                    REQUESTER: "",
                    FUNDING: "",
                    HOSTNAME: "",
                    HOSTJOBTITLE: "",
                    VICEHOSTNAME: "",
                    VICEHOSTJOBTITLE: "",
                    CONTACTPERSON: "",
                    CONTACTNUMBER: "",
                    PURPOSE: ""
                }
            });

            ///===== api 
            const reqData = reactive({
                TokenID: "",
                Token: "",
                Page: "",
                apmId:""
            });
            const apiGetApplyDetail = (data) => apiHander.userRequest.post("api/Apply/GetApplyDetail", data);
            const getApplyDetail = (data) => {
                apiGetApplyDetail(data)
                    .then((res) => {
                        const json = res.data;
                        if (json.Status) {
                            planDetail.value = JSON.parse(json.Data);
                        }
                        else
                            planDetail.value = [];
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }

            //=== 初始化
            onMounted(() => {
                if (sessionStorage.getItem("TokenID") === null) {
                    alert("請登入系統!!");
                    route.push({ path: "/Login" });
                }
                else if (sessionStorage.getItem("planDetail") != null) {
                    planDetail.value = JSON.parse(sessionStorage.getItem("planDetail"));
                }
                else {
                    if (route.query.apmId != null)
                        reqData.apmId = route.query.apmId;
                    apiHander.FunctionToken(getApplyDetail, reqData);
                }
            })

            //=== public
            return {               
                planText,
                planDetail,
                saveDetail,
                removeDetail,
                toHome
            }
        }
    })
</script>
<style lang="postcss" scoped>
    .logon {
        @apply flex justify-center min-h-screen   text-justify	;
    }
</style>