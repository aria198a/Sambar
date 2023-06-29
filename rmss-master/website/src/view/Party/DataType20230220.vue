
<template>
    <!--<loading :active.sync="isLoading"></loading>-->
    <div class="container mx-auto px-6 pb-12 sm:px-0">
        <h1 class="step-title">
            <template v-if="$route.query.setup === 'type'">
                步驟1、按資料類型設定您的資料分享偏好
            </template>
            <template v-else-if="$route.query.setup === 'range'">
                步驟2、按資料再利用的類型設定您的資料分享偏好
            </template>
            <template v-else>
                直接修改設定 
            </template>
        </h1>

        <!-- 版本一 -->
        <template v-if="($route.query.setup === 'type' || $route.query.setup === 'range')">
            <ul class="steps grid my-8">
                <li class="step" :class="{ 'step-primary': $route.query.setup === 'type' || $route.query.setup === 'range' }">資料類型設定</li>
                <li class="step" :class="{ 'step-primary': $route.query.setup === 'range' }">資料再利用設定</li>
                <li class="step">偏好設定一覽表</li>
            </ul>
            <div class="divider"></div> 
        </template>
        <div v-if="$route.query.setup === 'type' || $route.query.setup === 'range' || $route.query.setup === 'unit'" class="radio-box" >
            <div class="grid-table grid grid-cols-12 ">
                <div class="col-span-8 th"></div>
                <div class="mid th"><label
                        class="w-full sm:w-auto  tooltip-bottom space-x-2 tooltip sm:tooltip-bottom"
                        :data-tip="openTip.full">完全開放</label></div>
                <div class="mid th"><label class="w-full sm:w-auto  space-x-2 tooltip sm:tooltip-left"
                        :data-tip="openTip.non">完全不開放</label></div>
                <div class="mid th "><label
                        class="w-full sm:w-auto inline-flex tooltip-bottom space-x-2 tooltip sm:tooltip-bottom"
                        :data-tip="openTip.step">逐次決定</label></div>
                <div class="mid th"><label
                        class="w-full sm:w-auto inline-flex tooltip-bottom space-x-2 tooltip sm:tooltip-bottom"
                        :data-tip="openTip.select">選擇性開放</label></div>
                <template v-for="(iteml1, index1) in getTitle()" :key="index1">
                    <div class="col-span-12 question">
                        {{ iteml1 }}
                    </div>
                    <template v-for="(iteml2, index) in getCont(iteml1)" :key="index">
                        <div class="col-span-1 mid justify-center font-bold">
                            {{ iteml2.DC_NUM }}
                        </div>
                        <div class="col-span-7">
                            <label v-html="iteml2.CCONTENT"></label>
                        </div>
                        <template v-if="$route.query.setup !== 'unit'">
                            <div class="radioBlock"><input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="1" v-model="iteml2.ANS" @click="funAllNot(true, iteml2)" /></div>
                            <div class="radioBlock"><input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="4" v-model="iteml2.ANS" @click="funAllNot(false, iteml2)" /></div>
                            <div class="radioBlock"><input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="3" v-model="iteml2.ANS"/></div>
                            <div class="radioBlock"><input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="2" @click="handleShowCheck(iteml2.DC_NUM, iteml2)" v-model="iteml2.ANS" /></div>
                        </template>
                        <template v-else>
                            <div class="radioBlock">
                                <input 
                                    type="radio"
                                    :name="'radio-' + iteml2.DC_NUM"
                                    class="radio"
                                    value="1"
                                    :checked="checked(1, iteml2)"
                                    @click="funAllNot2(1, iteml2)"
                                /></div>
                            <div class="radioBlock">
                                <input 
                                    type="radio"
                                    :name="'radio-' + iteml2.DC_NUM"
                                    class="radio"
                                    value="4"
                                    :checked="checked(4, iteml2)"
                                    @click="funAllNot2(4, iteml2)"
                                /></div>
                            <div class="radioBlock">
                                <input 
                                    type="radio" 
                                    :name="'radio-' + iteml2.DC_NUM" 
                                    class="radio" 
                                    value="3"
                                    :checked="checked(3, iteml2)"
                                    @click="funAllNot2(3, iteml2)" 

                                /></div>
                            <div class="radioBlock">
                                <input 
                                    type="radio" 
                                    :name="'radio-' + iteml2.DC_NUM"
                                    class="radio" 
                                    value="2"
                                    :checked="checked(2, iteml2)"
                                    @click="handleShowCheck(iteml2.DC_NUM, iteml2)" 
                                />
                            </div>
                        </template>
                    </template>
                </template>
            </div>
        </div>
        <!-- <UsageTable v-if="$route.query.setup === 'unit'" :dataContSubList="unitDataSub"></UsageTable> -->

        <div class="divider"></div>

        <div class="space-x-4 text-center">
            <!-- <router-link v-if="$route.query.setup == 'range' " class="btn btn-outline w-40" :to="{path: '/dataType', query: { setup: 'type' }}">上一步</router-link>-->
            <!--<router-link v-if="$route.query.setup == 'type' " class="btn w-40" :to="{path: '/dataType', query: { setup: 'range' }}" @click="initData()">下一步</router-link>-->
            <button v-if="$route.query.setup == 'range'" class="btn w-40 mb-3" @click="goType()">上一步</button>
            <button v-if="$route.query.setup == 'type'" class="btn w-40" @click="goRange()">下一步</button>
            <button v-if="$route.query.setup == 'range'" class="btn w-40" @click="TempContSub()">下一步</button>
            <!--<router-link v-if="$route.query.setup == 'range' " class="btn w-40" :to="{path: '/dataUsage'} " @click="TempContSub()">下一步</router-link>-->
            <router-link v-if="$route.query.setup == 'unit'" class="btn btn-outline w-40"
                :to="{ path: '/dataUsage' }" @click="TempContSub()">返回</router-link>
            <!-- <router-link v-if="$route.query.setup == 'unit' " class="btn w-40" :to="{path: '/dataUsage'}">更新</router-link>-->
        </div>
    </div>
    <input type="checkbox" id="my-modal" class="modal-toggle" v-model="showCheckModal[0]" />
    <div class="modal">
        <div class="modal-box max-w-4xl">
            <template v-if="$route.query.setup === 'type' || showCheckTitle <= 5">
                <h3 class="font-bold text-lg mb-4">
                    您選擇「選擇性開放」，意即：<span class=" text-red-500">{{ selectTitle[showCheckTitle] }}</span>，僅開放與下列疾病或健康狀態有關者。（可複選）
                </h3>
            </template>
            <template v-else-if="$route.query.setup === 'range'">
                <h3 class="font-bold text-lg mb-4">
                    您選擇「選擇性開放」，意即：<span class=" text-red-500">{{ selectTitle[showCheckTitle] }}</span>的研究計畫應符合以下研究方向，方可利用您的資料。（可複選）
                </h3>
            </template>
            <div class="checkedList">
                <template v-for="(item, index) in dataSub.value" :key="index">
                    <div class=" tooltip-top tooltip w-full hover:bg-gray-200 rounded-md my-1" :data-tip="item.CDESC">
                        <div class="grid grid-cols-12 items-center">
                            <strong class="sm:col-span-6 col-span-12 font-bold text-primary-600 text-left py-2">{{ item.CNAME }}</strong>
                            <label class="sm:col-span-3 col-span-6 sm:w-auto inline-flex z-1">
                                <input type="checkbox" class="checkbox checkbox-primary" @change="funCheckSubSel(dataConSubOne, index, 1)" v-model="item.ANS"/>
                                <strong class="font-bold text-primary-600 text-left px-2">同意</strong>
                            </label>
                            <label class="sm:col-span-3 col-span-6 sm:w-auto inline-flex">
                                <input type="checkbox" class="checkbox checkbox-primary" @change="funCheckSubSel(dataConSubOne, index, 0)" v-model="item.ANS2"/>
                                <strong class="font-bold text-primary-600 text-left px-2">不同意</strong>
                            </label>
                        </div>
                    </div>
                </template>
            </div>
            <div class="modal-action flex grid grid-cols-11 justify-between items-center">
                <div class="sm:col-span-2 col-span-11 text-center">
                    <label class="inline-flex mr-2" for="">
                        <input type="checkbox" class="checkbox checkbox-accent mr-1" v-model="subChkAll" @click="funSubSelAllNot(true)" />
                        全選
                    </label>
                    <label class="inline-flex" for="">
                        <input type="checkbox" class="checkbox checkbox-accent mr-1" v-model="subChkNo" @click="funSubSelAllNot(false)" />
                        全不選
                    </label>
                </div>
                <div class="sm:col-span-8 col-span-9 sm:text-center">
                    <span v-if="subChkAll">與以上任何一種疾病或健康狀態有關資料皆開放。<br/>勾選此項等同於「<span class="text-success">完全開放</span>」。</span>
                    <span v-if="subChkNo">無論資料是否與以上任一疾病或健康狀態有關皆不開放。<br/>勾選此項等同於「<span class="text-error">完全不開放</span>」。</span>
                </div>
                
                <div class="sm:col-span-1 col-span-2 my-2">
                    <label v-if="showCheckModal[1]" for="my-modal" class="btn" @click="funCheckSubSel(dataConSubOne, false)">確定</label>
                    <button v-else class="btn" @click="funCheckSubSel(dataConSubOne, false, 'buttonDisabled')">確定</button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { defineComponent, ref, onUpdated, onMounted, reactive } from 'vue';
import { useRouter } from 'vue-router';
import { useRoute } from 'vue-router';
import { apihandler } from '../../api/apiHandler.js';
import UsageTable from './components/UsageTable.vue';

export default defineComponent({
    components: { UsageTable },
    setup() {
        //----------屬性
        const route = useRoute();
        const router = useRouter();
        const showCheckModal = ref([false, true]); // 0:showMmodal, 1:modalButton
        const apiHander = apihandler();
        const showList = ref([]);
        const showCheck = ref([]);
        const showCheckTitle = ref(null);
        const reqData = reactive({
            TokenID: "",
            Token: "",
            Page: "",
        });
        const isLoading = ref(false);
        const dataContSub = reactive([]);
        const dataSub = reactive([]);
        const unitDataSub = reactive([]);
        const showContSub = reactive([]);
        const dataConSubOne = reactive([]);
        const subChkAll = ref(false);
        const subChkNo = ref(false);
        const openTip = ref({
            full: "代表您同意完全開放您的資料被再利用。",
            step: "代表平台應逐次詢問您的開放意願。",
            select: "代表在符合特殊目的或情境下，您同意開放特定性質的資料被再利用。",
            non: "代表您不同意您的資料被再利用。"
        });
        const selectTitle = ref([
            "", 
            "您因診斷或治療疾病所需而生的紀錄資料中",
            "您因診斷或治療疾病所需而被採集的檢體中", 
            "您因參加預防健檢而衍生出的紀錄資料中", 
            "您因參加預防健檢而被採集的檢體中", 
            "您因參與學術研究而衍生出的紀錄資料中", 
            "您因參與學術研究而被採集的檢體中", 
            "受本國公部門補助的研究計畫應符合以下研究方向", 
            "非受本國公部門補助的研究計畫應符合以下研究方向", 
            "預期有商業目的的研究計畫應符合以下研究方向", 
            "預期僅用於學術目的的研究計畫應符合以下研究方向", 
            "未涉及跨境合作的研究計畫應符合以下研究方向", 
            "涉及跨境合作的研究計畫應符合以下研究方向"
        ]);
        // 初始
        const initData = () => {
            if (route.query.setup == "range") { //資料利用
                showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM02");
            }
            else if (route.query.setup == "type") { //資料類型
                showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM01");
            }
            else { // 一覽表進來的
                if (route.query.dcids == null) {
                    alert("資料載入錯誤，請重新開啟!");
                    router.push({ path: "./dataUsage" });
                }
                else {
                    const arrDcids = route.query.dcids.split(",");
                    showContSub.value = dataContSub.value.filter(item => arrDcids.includes(item.DC_ID));
                }
            }
        };
        // 確認是否已登入
        const userSignCheck = () => {
            if (sessionStorage.getItem("TokenID") === null) {
                alert("請登入系統!!");
                router.push({ path: "/Login" });
            }
            else {
                //apiHander.FunctionToken(checkPrivated, reqData);
                if (sessionStorage.getItem("ConSub") == null) { // 取得 題目與選擇性開放題目答案
                    apiHander.FunctionToken(getPriCont, reqData);
                    apiHander.FunctionToken(getPriContOther, reqData);
                }
                else {
                    dataContSub.value = JSON.parse(sessionStorage.getItem("ConSub"));
                    initData();
                }
            }
        };
        // 儲存ContSub 至session
        const TempContSub = () => {
            let isAllWrite = 1;
            showContSub.value.forEach(r => {
                if (r.ANS === 0) {
                    isAllWrite = 0;
                }
            });
            // all set need to setting
            if (isAllWrite === 1) {
                if (list.length) {
                    let ConSub = JSON.parse(sessionStorage.getItem("ConSub"));

                    //與全部相比是不是不一樣
                    var count = 0;
                    for(let i in list){
                        let Intersection = ConSub.filter((e) => {
                            return  (e.DC_ID == list[i].dcId && e.ANS == list[i].ANS)
                        })
                        if(Intersection.length > 0){
                            count+=1
                        }
                    }

                    
                    let dcidsStr = route.query.dcids;
                    let ConSubOther = sessionStorage.getItem("ConSubOther")
                    if(ConSubOther == undefined || ConSubOther == null){
                        ConSubOther = []
                        sessionStorage.setItem("ConSubOther",JSON.stringify(ConSubOther));
                    }else{
                        ConSubOther = JSON.parse(ConSubOther)
                    }

                    
                    //數量如果不一樣 代表有更動
                    if(count != list.length){
                        //單格暫存總管理沒有則新增進去
                        if(!ConSubOther.includes(dcidsStr))
                            ConSubOther.push(dcidsStr)

                        sessionStorage.setItem("ConSubOther",JSON.stringify(ConSubOther));
                        sessionStorage.setItem(dcidsStr, JSON.stringify(list));
                    }else{
                        //單格暫存總管理有則重新建立
                        if(ConSubOther.includes(dcidsStr)){
                            let ConSubOtherTemp = []
                            for(let i in ConSubOther){
                                if(ConSubOther[i] != dcidsStr)
                                    ConSubOtherTemp.push(ConSubOther[i])
                            }
                            ConSubOther = ConSubOtherTemp
                        }
                        
                        sessionStorage.setItem("ConSubOther",JSON.stringify(ConSubOther));
                        //刪除單格暫存
                        sessionStorage.removeItem(dcidsStr)
                    }
                }else 

                sessionStorage.setItem("ConSub", JSON.stringify(dataContSub.value));

                router.push({ path: "/dataUsage" });
            }
            else
                alert("尚有資料未填! 請確認填寫後至下一頁");
        };


        // 取得標題列表
        const getTitle = () => {
            if (showContSub.value != null) {
                // const titleList = showContSub.value.filter(item => item.DC_MAIN == pageType.value);
                const titleList = showContSub.value.map(item => item.CTITLE).filter(onlyUnique);
                // console.log(JSON.stringify(titleList));
                return titleList;
            }
            else
                return;
        };
        // 依標題，取得題目內容
        const getCont = (ctitle) => {
            if (showContSub.value != null) {
                const contList = showContSub.value.filter(item => item.CTITLE == ctitle);
                contList.forEach(item => { item.ACOTHER = item.ACOTHER == "" ? "50" : item.ACOTHER; });
                return contList;
            }
            else
                return;
        };
        //array distinct
        const onlyUnique = (value, index, self) => {
            return self.indexOf(value) === index;
        };
        // 按鈕 開啟選擇性開放
        const handleShowCheck = (titleNum, conSubOne) => {
            if (conSubOne.ANS != 2) {
                conSubOne.ANS_SUB.map(item => {
                    item.ANS = false,
                    item.ANS2 = false
                })
            }
            dataSub.value = conSubOne.ANS_SUB;
            dataConSubOne.value = conSubOne;
            showCheckTitle.value = titleNum;
            showCheckModal.value[0] = true;
            subChkAll.value = false;
            subChkNo.value = false;
            funCheckSubSel(dataConSubOne, false);
        };
        // 按鈕 至資料使用設定頁
        const goRange = () => {
            let isAllWrite = 1;
            showContSub.value.forEach(r => {
                if (r.ANS === 0) {
                    isAllWrite = 0;
                }
            });
            // all set need to setting
            if (isAllWrite === 1) {
                router.push({ path: "/dataType", query: { setup: "range" } });
                showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM02");
            }
            else
                alert("尚有資料未填! 請確認填寫後至下一頁");
        };
        // 按鈕 至資料類型設定頁
        const goType = () => {
            router.push({ path: "/dataType", query: { setup: "type", question: 1 } });
            showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM01");
        };
        // 主選項 選擇開放與不開放
        const funAllNot = (isAll, conSubOne) => {
            dataConSubOne.value = conSubOne;
            // funSubSelAllNot(isAll);
        };
        // 子選項 全選/全不選
        const funSubSelAllNot = (isSel) => {
            if (isSel) {
                subChkNo.value = false;
                dataConSubOne.value.ANS_SUB.forEach(function (sub) {
                    if (subChkAll.value) {
                        sub.ANS = false;
                        sub.ANS2 = false;
                    }else {
                        sub.ANS = true;
                        sub.ANS2 = false;
                    }
                });
            }
            else if (!isSel) {
                subChkAll.value = false;
                dataConSubOne.value.ANS_SUB.forEach(function (sub) {
                    if (subChkNo.value) {
                        sub.ANS = false;
                        sub.ANS2 = false;
                    }else {
                        sub.ANS = false;
                        sub.ANS2 = true;
                    }
                });
            }
            funCheckSubSel(dataConSubOne, false);
        };
        // 子項 判斷全選與否
        const funCheckSubSel = (inConSubOne, unAgree, value) => {
            if (inConSubOne == null || inConSubOne.value.length == 0)
                return;
            if (value == "buttonDisabled") {
                alert("請勾選同意或不同意")
                return;
            }

            // 同意與不同意 連動
            if (unAgree !=false || unAgree === 0) {
                if (value) {
                    inConSubOne.value.ANS_SUB[unAgree].ANS2 = !inConSubOne.value.ANS_SUB[unAgree].ANS;
                }else 
                    inConSubOne.value.ANS_SUB[unAgree].ANS = !inConSubOne.value.ANS_SUB[unAgree].ANS2;
            }

            // 全選與全部選 連動
            const chkSubNum = inConSubOne.value.ANS_SUB.filter(item => item.ANS).length;
            const chkSubNum2 = inConSubOne.value.ANS_SUB.filter(item => item.ANS2).length;
            // console.log(chkSubNum + ',' + inConSubOne.value.ANS_SUB.length)
            if (chkSubNum == 0 && chkSubNum2 == inConSubOne.value.ANS_SUB.length) {
                subChkAll.value = false;
                subChkNo.value = true;
                inConSubOne.value.ANS = 4;
            }
            else if (chkSubNum == inConSubOne.value.ANS_SUB.length) {
                subChkAll.value = true;
                subChkNo.value = false;
                inConSubOne.value.ANS = 1;
            }
            else {
                subChkAll.value = false;
                subChkNo.value = false;
                inConSubOne.value.ANS = 2;
            }

            // 判斷是否選擇

            chkSubNum+chkSubNum2 == inConSubOne.value.ANS_SUB.length ? showCheckModal.value[1] = true : showCheckModal.value[1] = false;
            // sessionStorage.setItem("ConSub", JSON.stringify(dataContSub.value));
            if(route.query.setup == 'unit') 
                funAllNot2(false, inConSubOne.value)
        };

        let list = [];
        const funAllNot2 = (value, conSubOne) => {
            // console.log(value, conSubOne,987897)
            const arrDcids = route.query.dcids.split(",");
            arrDcids.map((dcIds, index) => {
                if (dcIds == conSubOne.DC_ID) {
                    // console.log(conSubOne.ANS_SUB )
                    list[index].ANS = value ? value : conSubOne.ANS

                    if (list[index].ANS == 2) {
                        let ANSInfo = "";
                        let a = conSubOne.ANS_SUB.filter(x => x.ANS == true);
                        a.map((y, idx) => {
                            if (idx+1 == a.length) {
                                ANSInfo +=  `${y.CNAME}`
                            }else
                                ANSInfo +=  `${y.CNAME},`

                            list[index].Name = y.DS_MAIN
                        })

                        list[index].ANS_SUB_OPTIONS = a 
                        list[index].ANS_SUB = ANSInfo
                    }
                    else{
                        list[index].ANS_SUB_OPTIONS = []
                        list[index].ANS_SUB = ""
                    }
                }
            })
        }

        const checked = (value, info) => {
            let checkedValue = false
            if (sessionStorage.getItem(route.query.dcids) != null) {
                const list2 = JSON.parse(sessionStorage.getItem(route.query.dcids))
                list2.map(x => {
                    if (x.dcId == info.DC_ID && x.ANS == value)
                        checkedValue = true
                })
            }else {
                unitANS.map(item => {
                    if (item.DC_ID == info.DC_ID && item.ANS == value)
                        checkedValue = true
                })
            }
            return checkedValue
        }

        const unitANS = [];
        const unitModel = () => {
            unitDataSub.value = dataSub.value
            let dcidsStr = route.query.dcids;
            const arrDcids = route.query.dcids.split(",");
            arrDcids.map(dcIds => {
                unitANS.push(dataContSub.value.filter(item => item.DC_ID == dcIds)[0])
            })

            if (list.length == 0) {
                arrDcids.map((dcIds, index) => {
                    list[index] = {
                        "dcId": dcIds,
                        "ANS": unitANS[index].ANS,
                    }
                })
            }

            if (sessionStorage.getItem(dcidsStr) !=  null) {
                const list2 = JSON.parse(sessionStorage.getItem(dcidsStr))
                unitANS.map((item, idx) =>  {
                    if (item.dcId == list2[idx].DC_ID && item.ANS != list2[idx].ANS) {
                        unitANS[idx].ANS == list2[idx].ANS
                    }
                })
                list = list2
            }

        }

        // ---------- api
        const apiCheckPrivated = (data) => apiHander.userRequest.post("api/PrivacyPre/CheckPrivated", data);
        const apiGetPriCont = (data) => apiHander.userRequest.post("api/PrivacyPre/GetPriCont", data);
        const apiGetPriContOther = (data) => apiHander.userRequest.post("api/PrivacyPre/GetPriContOther", data);

        // 檢查是否有提交過 --無使用
        const checkPrivated = (data) => {
            apiCheckPrivated(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        let isPried = JSON.parse(json.Data).IsPried;
                        //console.log(isPried);
                        if (isPried) {
                            router.push({ path: "/" });
                        }
                        else {
                            if (sessionStorage.getItem("ConSub") == null) { // 取得 題目與選擇性開放題目答案
                                apiHander.FunctionToken(getPriCont, reqData);
                            }
                            else {
                                dataContSub.value = JSON.parse(sessionStorage.getItem("ConSub"));
                                initData();
                            }
                        }
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        };
        //取得題目答案
        const getPriCont = (data) => {
            apiGetPriCont(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        dataContSub.value = JSON.parse(json.Data);
                        initData();
                    }
                    else {
                        alert("請重新登入");
                        router.push({ path: "/Login" });
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        };
        const getPriContOther = (data) => {
            apiGetPriContOther(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        let data = JSON.parse(json.Data)

                        var ConSubOther = []
                        for(let i in data){
                            ConSubOther.push(data[i].name)
                            sessionStorage.setItem(data[i].name,JSON.stringify(data[i].content_list));
                        }
                        sessionStorage.setItem("ConSubOther",JSON.stringify(ConSubOther));


                        console.log(data)
                    }
                    else {
                        alert("請重新登入");
                        router.push({ path: "/Login" });
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        };
        //--------------週期事件
        onMounted(() => {
            userSignCheck();
            if(route.query.setup == 'unit') unitModel()
            //console.log("in")
        });
        onUpdated(() => {
            // initData();
            //if (route.query.setup == 'range') {
            //    pageType.value = "DM02";
            //    showList.value = dataUsage.value;
            //    showCheck.value = dataUsageCheck.value;
            //} else if (route.query.setup == 'type') {
            //    pageType.value = "DM01";
            //    showList.value = dataType.value;
            //    showCheck.value = dataTypeCheck.value;
            //} else {
            //    showList.value = dataUnit.value;
            //}
        });
        return {
            showList,
            showCheck,
            showCheckModal,
            handleShowCheck,
            showCheckTitle,
            isLoading,
            dataContSub,
            getTitle,
            getCont,
            dataSub,
            TempContSub,
            subChkAll,
            subChkNo,
            funSubSelAllNot,
            dataConSubOne,
            funCheckSubSel,
            funAllNot,
            goType,
            goRange,
            showContSub,
            openTip,
            selectTitle,
            funAllNot2,
            unitANS,
            checked,
            unitDataSub
        };
    },
})
</script>
<style lang="postcss" scoped>
.checkedList {
    label {
        @apply cursor-pointer;

        &::before {
            @apply z-30 p-4 rounded-lg
        }

    }
}
</style>