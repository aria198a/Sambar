<template>
    <div class="container mx-auto px-6 pb-12 sm:px-0">
        <div class="text-sm breadcrumbs">
            <ul>
                <li><a href="/">RMS</a></li>
                <li>匹配清單</li>
            </ul>
        </div>
        <h1 class="step-title">匹配清單</h1>
        <ul class="planInfo text-center">
            <li class="item"><span class="title">計畫編號</span><div class="content">{{applySingleInfo.number}}</div></li>
            <li class="item"><span class="title">計畫名稱</span><div class="content">{{applySingleInfo.planName}}</div></li>
            <li class="item">
                <span class="title">計畫類型</span>
                <div class="content">{{applySingleInfo.planType1}} / {{applySingleInfo.planType2}} / {{applySingleInfo.planType3}}</div>
            </li>
            <!-- <li class="item"><span class="title">計畫類型</span><div class="content">{{applySingleInfo.planType2}}</div></li>
            <li class="item"><span class="title">計畫類型</span><div class="content">{{applySingleInfo.planType3}}</div></li> -->
            <li class="item"><span class="title">匹配資料</span><div class="content">{{applySingleInfo.match}}筆</div></li>
        </ul>
        <hr class="my-8 border-stone-400 border-dashed">
        <div class="filter ">
            <div class="form-control">
                <label class="label">
                    <span class="label-text">關鍵字</span>
                </label>
                <input type="text" placeholder="計畫編號、計畫名稱或計畫類型" class="input input-bordered" v-model="reqApplyMatch.keyWord" />
            </div>
            <div class="form-control">
                <label class="label">
                    <span class="label-text">資料來源</span>
                </label>
                <select class="select input-bordered" v-model="reqApplyMatch.source">
                    <option value="" selected>全部</option>
                    <option value="參與學術研究">參與學術研究</option>
                    <option value="現有診斷治療">現有診斷治療</option>
                    <option value="預防健檢">預防健檢</option>
                </select>
            </div>
            <div class="form-control">
                <label class="label">
                    <span class="label-text">資料類型</span>
                </label>
                <select class="select input-bordered" v-model="reqApplyMatch.type">
                    <option value="" selected>全部</option>
                    <option value="資料紀錄">資料紀錄</option>
                    <option value="檢體">檢體</option>
                </select>
            </div>
            <div class="form-control" v-show="false">
                <label class="label">
                    <span class="label-text">建檔起日</span>
                </label>
                <input type="date" class="input input-bordered"  v-model="reqApplyMatch.sourceDataSt" />
            </div>
            <div class="form-control mb-4 sm:mb-0"  v-show="false">
                <label class="label">
                    <span class="label-text">建檔訖日</span>
                </label>
                <input type="date" class="input input-bordered" v-model="reqApplyMatch.sourceDataEnd" />
            </div>
            <button class="btn place-self-start sm:place-self-end  w-24" @click="queryApply">搜尋</button>
            
        </div>
        <div class="filter ">
            <p class="mb-3" style="width:15%">共 <strong class=" text-red-500">{{applySingleInfo.match}}</strong> 筆匹配資料</p>
            <div style="width:85%">
                <template v-if="getCheckedCount > 0">
                    <div  class="btn place-self-start sm:place-self-end w-24 float-right" @click="applyedBatch">申請</div>
                </template>
                <template v-else>
                    <div disabled class="btn place-self-start sm:place-self-end w-24 float-right">申請</div>
                </template>
            </div>
        
        </div>
        <div class="overflow-x-auto mb-6">
            <table class="table w-full">
                <!-- head -->
                <thead class="hidden sm:table-header-group">
                    <tr>
                        <th>項次</th>
                        <th>全部<input style="margin: 0.5rem;" type="checkbox" v-model="allChecked"  @change="allCheckedSwitch"/></th>
                        <th>資料編號</th>
                        <th>資料來源</th>
                        <th>資料類型</th>
                        <!--<th>建檔日期</th>-->
                        <th>偏好設定</th>
                        <th class="w-px">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in matchList" :key="index" class="table-item">
                        <td class="value"><span>項次</span>{{index+1}}</td>
                        <td class="value"><span>勾選</span>
                            <input v-if="item.state === 0" type="checkbox"  v-model="item.checked" />
                        </td>
                        <td class="value"><span>資料編號</span>{{item.number}}</td>
                        <td class="value"><span>資料來源</span>{{item.source}}</td>
                        <td class="value"><span>資料類型</span>{{item.type}}</td>
                        <!--<td class="value"><span>建檔日期</span>{{item.date}}</td>-->
                        <td class="value"><span>偏好設定</span>{{item.preference}}</td>
                        <td class="space-x-2 text-center">
                            <template v-if="item.state === 0">
                                <button class="btn w-24" @click="applyed(item.acId)">申請</button>
                            </template>
                            <template v-else-if="item.state === 1">
                                <span>申請中</span>
                            </template>
                            <template v-else-if="item.state === 2">
                                <span class=" text-green-500">已通過</span>
                            </template>
                            <template v-else-if="item.state === 3">
                                <span class=" text-red-500">已拒絕</span>
                            </template>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--<div class="pagination mb-8">
            <button class="">«</button>
            <button class="">‹</button>
            <button class="btn-active">1</button>
            <button class="">›</button>
            <button class="">»</button>
        </div>-->
        <div class="text-center">
            <router-link to="/" class="btn w-24">返回</router-link>
        </div>
      
    </div>
</template>
<script>
    import { defineComponent, reactive, ref, onMounted, onBeforeMount,computed } from 'vue';
    import { useRouter } from 'vue-router';
    import { useRoute } from 'vue-router';
    import { apihandler } from '../../api/apiHandler.js';

    export default defineComponent({
        setup() {
            const router = useRouter();
            const apiHander = apihandler();
            const route = useRoute();
            //===== model
            const allChecked = ref(false);
            const apmId = ref("");
            const matchList = ref([
                {
                    number: '2022083000003',
                    source: '現有診斷治療',
                    type: '資料紀錄',
                    date: '2021/01/21',
                    preference: '符合',
                    state: 0
                },
                {
                    number: '2022083000002',
                    source: '預防健檢',
                    type: '檢體',
                    date: '2022/08/21',
                    preference: '逐次決定',
                    state: 0
                },
                {
                    number: '2022083000001',
                    source: '參與學術研究',
                    type: '資料紀錄',
                    date: '2021/05/21',
                    preference: '符合',
                    state: 0
                },
                {
                    number: '2022073000001',
                    source: '參與學術研究',
                    type: '資料紀錄',
                    date: '2021/05/21',
                    preference: '符合',
                    state: 1
                },
                {
                    number: '2022073000001',
                    source: '參與學術研究',
                    type: '檢體',
                    date: '2021/05/21',
                    preference: '逐次決定',
                    state: 2
                },
                {
                    number: '2022073000001',
                    source: '參與學術研究',
                    type: '資料紀錄',
                    date: '2021/05/21',
                    preference: '逐次決定',
                    state: 3
                },
            ]);
            const applySingleInfo = ref({});

            //=== 事件
            const applyed = (acid) => {
                reqAddStage.value.acId = acid;
                reqAddStage.value.apmId = apmId.value;
                apiHander.FunctionToken(addApplyState, reqAddStage.value);
            }

            const applyedBatch = () => {
                var applyedInfos = []
                for(var i in matchList.value){
                    if(matchList.value[i].checked){
                        applyedInfos.push({
                            acId:matchList.value[i].acId,
                            apmId:matchList.value[i].apmId,
                            state:0
                        })
                    }
                }
                // console.log(applyedInfos)
                apiHander.FunctionToken(addApplyStateBatch,{applyedInfos:applyedInfos});
            }

            const queryApply = () => {
                if (reqApplyMatch.value.sourceDataSt == "")
                    reqApplyMatch.value.sourceDataSt = null;

                if (reqApplyMatch.value.sourceDataEnd == "")
                    reqApplyMatch.value.sourceDataEnd = null;

                apiHander.FunctionToken(getApplyMatchList, reqApplyMatch.value);
            }

            const allCheckedSwitch = () => {
                for(var i in matchList.value){
                    if(matchList.value[i].state === 0)
                        matchList.value[i].checked = allChecked.value
                }
            }

            const getCheckedCount = computed(() => {
                var count = 0
                for(var i in matchList.value){
                    if(matchList.value[i].checked)
                        count+=1
                }
                return count
            }) 



            ///===== api
            const reqApplySingle = ref({
                TokenID: "",
                Token: "",
                Page: "",
                apmId: apmId.value
            });
            const apiGetApplySingle = (data) => apiHander.userRequest.post("api/Apply/GetApplySingle", data);
            const getApplySingle = (data) => {
                apiGetApplySingle(data)
                    .then((res) => {
                        const json = res.data;
                        if (json.Status) {
                            applySingleInfo.value = JSON.parse(json.Data);
                        }                       
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }

            // 取得匹配清單
            const reqApplyMatch = ref({
                TokenID: "",
                Token: "",
                Page: "",
                apmId: apmId.value,
                keyWord: "",
                source: "",
                type: "",
                sourceDataSt: null,
                sourceDataEnd: null
            });
            const apiGetApplyMatchList = (data) => apiHander.userRequest.post("api/Apply/GetApplyMatchList", data);
            const getApplyMatchList = (data) => {
                apiGetApplyMatchList(data)
                    .then((res) => {
                        const json = res.data;
                        if (json.Status) {
                            var jsdata = JSON.parse(json.Data)
                            for(var i in jsdata){
                                jsdata[i].checked = false
                            }
                            
                            matchList.value = jsdata;
                            
                        }                        
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }
            // 申請更新 狀態
            const reqAddStage = ref({
                TokenID: "",
                Token: "",
                Page: "",
                apmId: apmId.value,
                acId: "",
                state:1
            });
            const apiAddApplyState = (data) => apiHander.userRequest.post("api/Apply/AddApplyState", data);
            const addApplyState = (data) => {
                apiAddApplyState(data)
                    .then((res) => {
                        const json = res.data;
                        if (json.Status) {
                            alert("已提送申請");
                            apiHander.FunctionToken(getApplyMatchList, reqApplyMatch.value);
                        }
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }

            const apiAddApplyStateBatch = (data) => apiHander.userRequest.post("api/Apply/AddApplyStateBatch", data);
            const addApplyStateBatch = (data) => {
                apiAddApplyStateBatch(data)
                    .then((res) => {
                        const json = res.data;
                        if (json.Status) {
                            alert("已提送申請");
                            apiHander.FunctionToken(getApplyMatchList, reqApplyMatch.value);
                        }
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }

            //=== 初始化
            onBeforeMount(() => {
                if (sessionStorage.getItem("TokenID") === null) {
                    alert("請登入系統!!");
                    router.push({ path: "/Login" });
                }
                if (route.query.apmId == null) {
                    router.push({ path: "/Login" });
                }                   
                else {
                    apmId.value = route.query.apmId;
                    reqApplySingle.value.apmId = apmId.value;
                    reqApplyMatch.value.apmId = apmId.value;
                    apiHander.FunctionToken(getApplySingle, reqApplySingle.value);
                    apiHander.FunctionToken(getApplyMatchList, reqApplyMatch.value);
                }
            })

            return {
                matchList,
                reqApplyMatch,
                applySingleInfo,
                applyed,
                allChecked,
                allCheckedSwitch,
                queryApply,
                getCheckedCount,
                applyedBatch,

            }
        }
    })
</script>
<style lang="postcss" scoped>
    .planInfo {
        @apply flex flex-col sm:flex-row border border-stone-400 bg-white mb-8 rounded-lg overflow-hidden;
        .item

    {
        @apply flex-1 border-l border-stone-400 first:border-none;
        .title

    {
        @apply px-4 py-2 border-b border-stone-400 block bg-primary-300 text-primary-800 text-sm font-bold;
    }

    .content {
        @apply px-4 py-2 text-center text-lg
    }

    }
    }
</style>