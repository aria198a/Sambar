
<template>
    <!--<loading :active.sync="isLoading"></loading>-->
    <div class="container mx-auto px-6 pb-12 sm:px-0">
        <h1 class="step-title">
            <template v-if="$route.query.setup === 'type'">
                按資料類型設定您的資料分享偏好
            </template>
            <template v-else-if="$route.query.setup === 'range'">
                按資料再利用的類型設定您的資料分享偏好
            </template>
        </h1>

        <!-- 版本二 -->
        <template v-if="($route.query.setup === 'type' || $route.query.setup === 'range')">
            <ul class="steps grid my-8">
                <li data-content="" class="step step-primary">資料類型設定</li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' || $route.query.question != 1}"></li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' || $route.query.question == 3}"></li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range'}">資料再利用設定</li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' && $route.query.question != 1}"></li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' && $route.query.question == 3}"></li>
                <li data-content="" class="step">偏好設定一覽表</li>
            </ul>
            <div class="divider"></div> 
        </template>
        <div>
            <!-- <h3 class="text-3xl mb-6 text-primary-700 text-center">{{ getTitle($route.query.question) }}</h3> -->
            <template v-if="$route.query.setup == 'type'">
                <h3 v-if="$route.query.question == 1" class="text-3xl mb-6 text-primary-700 text-center">診斷或治療疾病為目的所產生的資料</h3>
                <h3 v-if="$route.query.question == 2" class="text-3xl mb-6 text-primary-700 text-center">健康檢查所產生的資料</h3>
                <h3 v-if="$route.query.question == 3" class="text-3xl mb-6 text-primary-700 text-center">參與學術研究所產生的資料</h3>
            </template>
            <template v-else-if="$route.query.setup == 'range'">
                <h3 v-if="$route.query.question == 1" class="text-3xl mb-6 text-primary-700 text-center">研究計畫的經費是由誰出錢進行</h3>
                <h3 v-if="$route.query.question == 2" class="text-3xl mb-6 text-primary-700 text-center">研究計畫的預期目的為何</h3>
                <h3 v-if="$route.query.question == 3" class="text-3xl mb-6 text-primary-700 text-center">研究計畫是否涉及跨境合作</h3>
            </template>
            <div class="flex flex-col lg:flex-row sm:space-x-12 items-center">
                <div class="w-full lg:w-1/3 ">
                    <template v-if="$route.query.setup == 'type'">
                        <div class="grid grid-cols-1 pt-6 pr-6 pb-3 rounded-lg text-center place-items-center">
                            <div class="col-span-1">
                                <img v-if="$route.query.question == 1" src="../../assets/image/path01.png" alt="" class="inline sm:w-8/12">
                                <img v-if="$route.query.question == 2" src="../../assets/image/path02.png" alt="" class="inline sm:w-8/12">
                                <img v-if="$route.query.question == 3" src="../../assets/image/path03.png" alt="" class="inline sm:w-8/12">
                            </div>
                            <div class="col-span-1 text-xl">
                                <div v-if="$route.query.question == 1">我們在看醫生的時候，醫生會進行問診、診斷與治療，因此所產生的資料</div>
                                <div v-if="$route.query.question == 2">我可能為了就學、就業而去做的預防健康檢查，或者是例行性的自費健檢，所產生的資料</div>
                                <div v-if="$route.query.question == 3">我參加了一些疾病或是疫苗等醫療相關的研究計畫，額外做了其他檢查或病理檢驗所產生的資料</div>
                            </div>
                        </div>
                        <!-- <img v-if="$route.query.question == 1" src="../assets/image/path01.png" alt="">
                        <img v-if="$route.query.question == 2" src="../assets/image/path02.png" alt="">
                        <img v-if="$route.query.question == 3" src="../assets/image/path03.png" alt=""> -->
                    </template>
                    <template v-if="$route.query.setup == 'range'">
                        <div class="grid grid-cols-1 pt-6 pr-6 pb-3 rounded-lg text-center place-items-center">
                            <div class="col-span-1">
                                <img v-if="$route.query.question == 1" src="../../assets/image/path04.png" alt="" class="inline w-8/12">
                                <img v-if="$route.query.question == 2" src="../../assets/image/path05.png" alt="" class="inline w-8/12">
                                <img v-if="$route.query.question == 3" src="../../assets/image/path06.png" alt="" class="inline w-8/12">
                            </div>
                            <div class="col-span-1 text-xl">
                                <div v-if="$route.query.question == 1">研究計畫的經費來源可能是政府、可能是非政府單位，我需要想一想「誰出錢」這件事，會不會影響我提供資料的意願？</div>
                                <div v-if="$route.query.question == 2">我的資料被研究計畫拿去用，是基於學術目的？還是有利益商業目的？</div>
                                <div v-if="$route.query.question == 3">研究計畫可能是本地的，也可能有境外機構或他國勢力之參與，並對資料進行蒐集、儲存、分析，我是否願意提供資料給他們再利用？</div>
                            </div>
                        </div>
                        <!-- <img v-if="$route.query.question == 1" src="../assets/image/path04.png" alt="">
                        <img v-if="$route.query.question == 2" src="../assets/image/path05.png" alt="">
                        <img v-if="$route.query.question == 3" src="../assets/image/path06.png" alt=""> -->
                    </template>
                </div>
                <div class="w-full lg:w-2/3">
                    <template v-for="(iteml2, index2) in getCont(getTitle($route.query.question))" :key="index2">
                        <div v-if="index2 == 0" class="text-xl my-12">
                            <template v-if="$route.query.setup == 'type'">
                                資料區分單純文字的紀錄資料與有生物資訊的檢體，請依您的意願勾選下方偏好設定:
                            </template>
                            <template v-else-if="$route.query.setup == 'range'">
                                <span v-if="$route.query.question == 1">經費來源區分本國公部門與非本國公部門之補助，請依您的意願勾選下方偏好設定：</span>
                                <span v-if="$route.query.question == 2">研究的預期目的區分商業目的為主與學術目的，請依您的意願勾選下方偏好設定：</span>
                                <span v-if="$route.query.question == 3">請依您的意願勾選下方偏好設定：</span>

                            </template>
                        </div>
                        <div class="grid sm:grid-rows-2 grid-cols-5 my-5 border pt-6 pr-6 pb-3 bg-white rounded-lg">
                            <div v-if="$route.query.setup == 'type'" class="col-span-5 px-3 text-xl">
                                Q{{ index2+1 }}: 
                                <span v-if="index2 == 0">紀錄資料</span>
                                <span v-else>檢體</span>
                            </div>
                            <div v-else-if="$route.query.setup == 'range'" class="col-span-5 px-3 text-xl">
                                Q{{ index2+1 }}: 
                                <template v-if="$route.query.question == 1">
                                    <span v-if="index2 == 0">公部門（例如中央或地方政府、公立大學／醫院）</span>
                                    <span v-else>非公部門（例如由民間企業、私立大學／醫院、或NGO等資助經費占50%以上）</span>
                                </template>
                                <template v-if="$route.query.question == 2">
                                    <span v-if="index2 == 0">商業目的（例如技術轉移收取權利金、研發或改良新型藥物或器材以銷售為目的）</span>
                                    <span v-else>學術目的（純粹學術研究、公部門之政策依據或立法研究）</span>
                                </template>
                                <template v-if="$route.query.question == 3">
                                    <span v-if="index2 == 0">研究計畫涉及跨境合作，舉凡經費來源、計畫規劃或執行過程有無境外機構或他國勢力的參與，甚至資料是由非中華民國台灣境內的人所蒐集、分析，均包括在內。</span>
                                    <span v-else>研究計畫未涉及任何跨國合作</span>
                                </template>
                            </div>
                            <!-- <div v-if="$route.query.setup == 'range'" class="sm:row-span-2 col-span-1 row-span-5 flex justify-center items-center stepNumber">{{ iteml2.DC_NUM }}</div>
                            <div v-if="$route.query.setup == 'range'" class="col-span-4"><div class="mb-4" v-html="iteml2.CCONTENT"></div></div> -->

                            <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="1" v-model="iteml2.ANS" @click="funAllNot(true, iteml2)" />
                                <span class="inline-block align-top ml-1">完全開放</span>
                            </div>
                            <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="4" v-model="iteml2.ANS" @click="funAllNot(false, iteml2)" />
                                <span class="inline-block align-top ml-1">完全不開放</span>
                            </div>
                            <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="3" v-model="iteml2.ANS" />
                                <span class="inline-block align-top ml-1">逐次決定</span>
                            </div>
                            <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" value="2" @click="handleShowCheck(iteml2.DC_NUM, iteml2)" v-model="iteml2.ANS" />
                                <span class="inline-block align-top ml-1">選擇性開放</span>
                            </div>
                        </div>
                    </template>
                </div>
            </div>
        </div>
        <div v-if="$route.query.setup === 'unit'" class="radio-box" >
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
                <template v-for="(iteml1, index1) in getTitleAll()" :key="index1">
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
        <div class="divider"></div> 


        <div class="space-x-4 text-center">
            <button v-if="($route.query.setup != 'unit') && !($route.query.setup == 'type' && $route.query.question == 1)" class="btn w-40 mb-3" @click="goType()">上一步</button>
            <button v-if="($route.query.setup != 'unit') && !($route.query.setup == 'range' && $route.query.question == 3)" class="btn w-40" @click="goRange()">下一步</button>
            <button v-if="$route.query.setup == 'range' && $route.query.question == 3" class="btn w-40" @click="TempContSub()">下一步</button>
            <router-link 
                v-if="$route.query.setup == 'unit'" 
                class="btn btn-outline w-40"
                :to="{ path: '/dataUsage' }"
                @click="TempContSub()"
            >返回</router-link>
        </div>
    </div>
    <input type="checkbox" id="my-modal" class="modal-toggle" v-model="showCheckModal[0]" />
    <div class="modal">
        <div class="modal-box max-w-4xl">
            <template v-if="$route.query.setup === 'type'">
                <h3 class="font-bold text-lg mb-4">
                    您選擇「選擇性開放」，意即：<span class=" text-red-500">{{ showCheckTitle }}</span>，僅開放與下列疾病或健康狀態有關者。（可複選）
                </h3>
            </template>
            <template v-else-if="$route.query.setup === 'range'">
                <h3 class="font-bold text-lg mb-4">
                    您選擇「選擇性開放」，意即：<span class=" text-red-500">{{ showCheckTitle }}</span>的研究計畫應符合以下研究方向，方可利用您的資料。（可複選）
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
import { useRouter } from 'vue-router'
import { useRoute } from 'vue-router'
import { apihandler } from '../../api/apiHandler.js';
export default defineComponent({
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
        //const pageType = ref("DM01");
        const isLoading = ref(false);
        const dataContSub = reactive([]);
        const dataSub = reactive([]);
        const unitDataSub = reactive([]);
        const showContSub = reactive([]);
        const dataConSubOne = reactive([]);
        const subChkAll = ref(false);
        const subChkNo = ref(false);
        const openTip = ref({ full: "代表您同意完全開放您的資料被再利用。", step: "代表平台應逐次詢問您的開放意願。", select: "代表在符合特殊目的或情境下，您同意開放特定性質的資料被再利用。", non: "代表您不同意您的資料被再利用。" });
        const selectTitle = ref(["", "您因診斷或治療疾病所需而生的紀錄資料中", "您因診斷或治療疾病所需而被採集的檢體中", "您因參加預防健檢而衍生出的紀錄資料中", "您因參加預防健檢而被採集的檢體中", "您因參與學術研究而衍生出的紀錄資料中", "您因參與學術研究而被採集的檢體中", "受本國公部門補助的研究計畫應符合以下研究方向", "非受本國公部門補助的研究計畫應符合以下研究方向", "預期有商業目的的研究計畫應符合以下研究方向", "預期僅用於學術目的的研究計畫應符合以下研究方向", "未涉及跨境合作的研究計畫應符合以下研究方向", "涉及跨境合作的研究計畫應符合以下研究方向"
        ]);
        //------功能
        // 初始
        const initData = () => {
            if (route.query.setup == "range") { //資料利用
                showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM02");
            }
            else if (route.query.setup == "type") { //資料類型=
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
                    // apiHander.FunctionToken(getPriCont, reqData);
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
            console.log(list)
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
                        console.log(Intersection)
                        if(Intersection.length > 0){
                            count+=1
                        }
                    }
                    
                    console.log(count)
                    console.log(ConSub)

                    
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
        const getTitle = (value) => {
            if (showContSub.value != null) {
                // const titleList = showContSub.value.filter(item => item.DC_MAIN == pageType.value);
                const titleList = showContSub.value.map(item => item.CTITLE).filter(onlyUnique);
                return titleList[value-1];
            }
            else
                return;
        };
        const getTitleAll = () => {
            if (showContSub.value != null) {
                // const titleList = showContSub.value.filter(item => item.DC_MAIN == pageType.value);
                const titleList = showContSub.value.map(item => item.CTITLE).filter(onlyUnique);
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
                contList.forEach(item => { item.CCONTENT = item.CCONTENT.replace("右側", "以下") });
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
            showCheckTitle.value = selectTitle.value[titleNum];
            showCheckModal.value[0] = true;
            subChkAll.value = false;
            subChkNo.value = false;
            funCheckSubSel(dataConSubOne, false);
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

            // 同意與不同意 關連
            if (unAgree !=false || unAgree === 0) {
                if (value) {
                    inConSubOne.value.ANS_SUB[unAgree].ANS2 = !inConSubOne.value.ANS_SUB[unAgree].ANS;
                }else 
                    inConSubOne.value.ANS_SUB[unAgree].ANS = !inConSubOne.value.ANS_SUB[unAgree].ANS2;
            }

            // 全選與全部選 關連
            const chkSubNum = inConSubOne.value.ANS_SUB.filter(item => item.ANS).length;//同意
            const chkSubNum2 = inConSubOne.value.ANS_SUB.filter(item => item.ANS2).length;//不同意
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

            // 判斷是否有勾選
            chkSubNum+chkSubNum2 == inConSubOne.value.ANS_SUB.length ? showCheckModal.value[1] = true : showCheckModal.value[1] = false;
            // sessionStorage.setItem("ConSub", JSON.stringify(dataContSub.value));
            if(route.query.setup == 'unit') 
                funAllNot2(false, inConSubOne.value)
        };

        // 按鈕 至資料使用設定頁
        const goRange = () => {
            let isAllWrite = 1;
            const isAllCheck = showContSub.value.filter(item => item.CTITLE == getTitle(route.query.question) && item.ANS !== 0)
            if (isAllCheck.length < 2)
                isAllWrite = 0

            // all set need to setting
            if (isAllWrite === 1) {
                if (route.query.question != 3) {
                    router.push({ path: "/dataType", query: { setup: route.query.setup , question: parseInt(route.query.question)+1 } });
                }else {
                    router.push({ path: "/dataType", query: { setup: "range" , question: 1} });
                    showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM02");
                }
            }
            else
                alert("尚有資料未填! 請確認填寫後至下一頁");
        };
        // 按鈕 至資料類型設定頁
        const goType = () => {
            if (route.query.setup == "type") {
                router.push({ path: "/dataType", query: { setup: "type", question: parseInt(route.query.question)-1 } });
            }else if (route.query.setup == "range") {
                if (route.query.question == 1) {
                    router.push({ path: "/dataType", query: { setup: "type", question: 3 } });
                    showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM01");
                }else {
                    router.push({ path: "/dataType", query: { setup: "range", question: parseInt(route.query.question)-1 } });
                }
            }
        };

        let list = [];
        const funAllNot2 = (value, conSubOne) => {
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
                        sessionStorage.setItem("ConSub", JSON.stringify(dataContSub.value));
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
                        
                        if(sessionStorage.getItem("ConSubOther") != null && sessionStorage.getItem("ConSubOther") != undefined){
                            var ConSubOtherTemp = JSON.parse(sessionStorage.getItem("ConSubOther"))
                            for(var i in ConSubOtherTemp){
                                sessionStorage.removeItem(ConSubOtherTemp[i]);
                            }
                        }

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
            //console.log("in")
            userSignCheck();
            if(route.query.setup == 'unit') unitModel()
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
            getTitleAll,
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
            funAllNot2,
            checked,
            unitModel,
            unitDataSub,
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