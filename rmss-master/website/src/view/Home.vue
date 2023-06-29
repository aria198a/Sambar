
<template>

    <div v-if="states.userType == 0 " class="container mx-auto px-6 pb-12 sm:px-0">
        <div class="text-sm breadcrumbs">
            <ul>
                <li>RMS</li>
                <li>資料再利用申請清單</li>
            </ul>
        </div>
        <h1 class="step-title">資料再利用申請清單</h1>
        <p style="margin: 0.5rem;">本清單所呈現的，是向您的資料提出申請利用的計畫列表；操作欄位訊息顯示之「開放提供」或「不開放提供」，代表與您的資料偏好設定媒合後的結果。</p>
        <p style="margin: 0.5rem;">若您有進行偏好的細項設定，則會再出現「選擇性開放」以及「逐次決定」的訊息，當出現「逐次決定」時，會需要請您再次確認是否拒絕或同意提供。</p>
        <div class="mb-6 flex flex-col sm:flex-row items-end sm:space-x-2">
            <div class="form-control">
                <label class="label">
                    <span class="label-text">關鍵字</span>
                </label>
                <input type="text" placeholder="計畫編號、計畫名稱或計畫類型" class="input input-bordered" style="width:300px" v-model="reqApplyed.keyWord"/>
            </div>
            <div class="form-control">
                <label class="label">
                    <span class="label-text">申請起日</span>
                </label>
                <input type="date" class="input input-bordered" v-model="reqApplyed.applyDataSt" />
            </div>
            <div class="form-control mb-4 sm:mb-0">
                <label class="label">
                    <span class="label-text">申請訖日</span>
                </label>
                <input type="date" class="input input-bordered" v-model="reqApplyed.applyDataEnd" />
            </div>
            <button class="btn place-self-start sm:place-self-end w-24" @click="queryApply">搜尋</button>
        </div>
        <p class="mb-3">共 {{usageList.length}} 筆計畫申請</p>
        <div class="overflow-x-auto mb-6 shadow">
            <table class="table w-full">
                <!-- head -->
                <thead class="hidden sm:table-header-group">
                    <tr>
                        <th>計畫編號</th>
                        <th>計畫名稱</th>
                        <th>計畫類型</th>
                        <th>申請資料來源</th>
                        <th>申請資料類型</th>
                        <th>申請日期</th>
                        <!-- <th>偏好設定</th> -->
                        <th class="w-px">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in usageList" :key="index" class="table-item">
                        <td class="value"><span>計畫編號</span>{{item.apmNo}}</td>
                        <td class="value"><span>計畫名稱</span>{{item.planName}}</td>
                        <td class="value"><span>計畫類型</span>{{item.planType}}</td>
                        <td class="value"><span>申請資料來源</span>{{item.applySource}}</td>
                        <td class="value"><span>申請資料類型</span>{{item.applyType}}</td>
                        <td class="value"><span>申請日期</span>{{dateConvert(item.applyDate)}}</td>
                        <!-- <td class="value"><span>偏好設定</span>{{item.preference}}</td> -->
                        <td class="text-center space-x-2">
                            <template v-if="item.state === 2">
                                <button class="btn btn-sm">研究成果</button>
                            </template>
                            <template v-else-if="item.state === 1">
                                <button class="btn btn-sm btn-error w-24" @click="updateStatus(item.apmId,item.acId,3)">拒絕</button>
                                <button class="btn btn-sm btn-success w-24" @click="updateStatus(item.apmId,item.acId,2)">同意</button>
                            </template>
                            <template v-else-if="item.state === 5">
                                <span class=" text-blue-500 w-24">已通過</span>
                            </template>
                            <template v-else-if="item.state === 3">
                                <span class=" text-red-500 w-24">已拒絕</span>
                            </template>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--<div class="pagination ">
            <button class="">«</button>
            <button class="">‹</button>
            <button class="">1</button>
            <button class="btn-active">2</button>
            <button class="">3</button>
            <button class="">›</button>
            <button class="">»</button>
        </div>-->
    </div>
    <div v-else-if="states.userType == 1 " class="container mx-auto px-6 pb-12 sm:px-0">
        <div class="text-sm breadcrumbs">
            <ul>
                <li>RMS</li>
                <li>研究計畫總覽</li>
            </ul>
        </div>
        <h1 class="step-title">研究計畫總覽</h1>
        <div class="mb-4 flex items-end justify-end space-x-2 sm:mx-4">
            <router-link to="/planInfo" class="btn btn-secondary w-24">計畫申請</router-link>
        </div>
        <div class="overflow-x-auto mb-6 shadow">
            <table class="table w-full">
                <!-- head -->
                <thead class=" hidden sm:table-header-group">
                    <tr>
                        <th>計畫編號</th>
                        <th>計畫名稱</th>
                        <th>計畫類型</th>
                        <th>計畫類型</th>
                        <th>計畫類型</th>
                        <th>匹配資料</th>
                        <th class="w-px">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in planList" :key="index" class="table-item">
                        <td class="value"><span>計畫編號</span>{{item.number}}</td>
                        <td class="value"><span>計畫名稱</span>{{item.planName}}</td>
                        <td class="value"><span>計畫類型</span>{{item.planType1}}</td>
                        <td class="value"><span>計畫類型</span>{{item.planType2}}</td>
                        <td class="value"><span>計畫類型</span>{{item.planType3}}</td>
                        <td class="value"><span>匹配資料</span>{{item.match}}</td>
                        <td class="space-x-1 flex sm:table-cell ">
                            <router-link :to="{path:'/planInfo', query:{apmId: item.apmId}}" class="btn btn-sm w-20 flex-1">計畫變更</router-link>
                            <router-link :to="{path:'/planMatch', query:{apmId: item.apmId}}" class="btn btn-sm w-20 flex-1">匹配清單</router-link>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--<div class="pagination ">
            <button class="">«</button>
            <button class="">‹</button>
            <button class="">1</button>
            <button class="btn-active">2</button>
            <button class="">3</button>
            <button class="">›</button>
            <button class="">»</button>
        </div>-->
    </div>

    <input  type="checkbox" id="alert-modal"  class="modal-toggle" v-model="localData.modal" />
    <label  for="alert-modal" class="modal cursor-pointer" style="background-color: rgba(0,0,0,0);">
        <label  class="modal-box max-w-xs" style="color: white;background-color: rgb(66 66 66 / 70%);">
            <label style="color:red;">請注意</label>，本帳號為測試帳號，故留有測試用之模擬資料
        </label >
    </label >
</template>
<script>
    import { defineComponent, computed, reactive, ref, onMounted } from 'vue';
    import { useStore } from "vuex";
    import { useRouter } from 'vue-router';
    import { apihandler } from '../api/apiHandler.js';

    export default defineComponent({
    setup() {        
        const router = useRouter();
        const apiHander = apihandler();
        const store = useStore();
        const localData = reactive({
            modal: true
        });
        const states = reactive({
            userType: ""
            //userType: computed(() => {
            //    store.state.auth.user.userType
            //}),
        });
        //=== model
        const usageList = ref([
            // {
            //     number: "2022083000002",
            //     planName: "公共衛生政策研究計畫",
            //     planType: "公共 / 非商業 / 國內",
            //     application: "2022/08/30",
            //     preference: "符合",
            //     state: 0
            // },
            // {
            //     number: "2022083000001",
            //     planName: "醫療機構的組織與架構研究計畫",
            //     planType: "非公共 / 商業 / 境外",
            //     application: "2022/08/29",
            //     preference: "逐次決定",
            //     state: 1
            // },
            // {
            //     number: "2022083000001",
            //     planName: "醫療機構的組織與架構研究計畫",
            //     planType: "非公共 / 商業 / 境外",
            //     application: "2022/08/29",
            //     preference: "逐次決定",
            //     state: 2
            // },
        ]);
        const planList = ref([]);
        //=== 功能
        // 查詢
        const queryApply = () => {
            if (reqApplyed.value.applyDataSt == "")
                reqApplyed.value.applyDataSt = null;
            if (reqApplyed.value.applyDataEnd == "")
                reqApplyed.value.applyDataEnd = null;
            apiHander.FunctionToken(getApplyedList, reqApplyed.value);
        };
        // 更新狀態
        const updateStatus = (apmid, acid, state) => {
            reqUpdateStage.value.apmId = apmid;
            reqUpdateStage.value.acId = acid;
            reqUpdateStage.value.state = state;
            apiHander.FunctionToken(updateApplyedState, reqUpdateStage.value);
        };
        // 日期轉換
        const dateConvert = (date) => {
            const mydate = new Date(date);
            const y = mydate.getFullYear();
            const m = (mydate.getMonth() + 1).toString().length == 1 ? "0" + (mydate.getMonth() + 1) : mydate.getMonth() + 1;
            const d = mydate.getDate().toString().length == 1 ? "0" + mydate.getDate() : mydate.getDate();
            return `${y}-${m}-${d}`;
        };
        ///===== api
        // 取得申請清單
        const reqData = reactive({
            TokenID: "",
            Token: "",
            Page: "",
        });
        const apiGetApplyList = (data) => apiHander.userRequest.post("api/Apply/GetApplyList", data);
        const getApplyList = (data) => {
            apiGetApplyList(data)
                .then((res) => {
                const json = res.data;
                if (json.Status) {
                    planList.value = JSON.parse(json.Data);
                }
                else
                    planList.value = [];
            })
                .catch((err) => {
                console.log(err);
            });
        };
        // 取得資料再利用清單
        const reqApplyed = ref({
            TokenID: "",
            Token: "",
            Page: "",
            keyWord: "",
            applyDataSt: null,
            applyDataEnd: null
        });
        const apiGetApplyedList = (data) => apiHander.userRequest.post("api/Apply/GetApplyedList", data);
        const getApplyedList = (data) => {
            apiGetApplyedList(data)
                .then((res) => {
                const json = res.data;
                if (json.Status) {
                    usageList.value = JSON.parse(json.Data);
                    // console.log(usageList.value)
                }
                else
                    usageList.value = [];
            })
                .catch((err) => {
                console.log(err);
            });
        };
        // 更新 狀態
        const reqUpdateStage = ref({
            TokenID: "",
            Token: "",
            Page: "",
            apmId: "",
            acId: "",
            state: 1
        });
        const apiUpdateApplyedState = (data) => apiHander.userRequest.post("api/Apply/UpdateApplyedState", data);
        const updateApplyedState = (data) => {
            apiUpdateApplyedState(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        alert("更新成功");
                        apiHander.FunctionToken(getApplyedList, reqApplyed.value);
                    }
                    else
                        alert("更新失敗");
                })
                .catch((err) => {
                    console.log(err);
                });
        };
        ///=== 初始
        onMounted(() => {
            apiHander._sessionData("planDetail", "");
            if (store.state.auth.user === null) {
                alert("請登入");
                router.push({ path: "/Login" });
            }
            else {
                states.userType = store.state.auth.user.userType;
                if (states.userType == 1)
                    apiHander.FunctionToken(getApplyList, reqData);
                else if (states.userType == 0)
                    apiHander.FunctionToken(getApplyedList, reqApplyed.value);
            }
        });

        //=== public
        return {
            usageList,
            states,
            planList,
            queryApply,
            updateStatus,
            reqApplyed,
            dateConvert,
            localData,
        };
    },
})
</script>