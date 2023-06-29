<template>
    <div class="container mx-auto px-6 pb-12 sm:px-0" >
        <!-- <h1 class="step-title" >步驟3、資料再利用偏好設定一覽表</h1>
        <ul class="steps grid my-8" >
            <li class="step step-primary">資料類型設定</li>
            <li class="step step-primary">資料再利用設定</li>
            <li class="step step-primary">偏好設定一覽表</li>
        </ul>
        <div class="divider"></div> -->

        <div class="mb-5">
            <template v-for="(item, index1) in usageList2" :key="index1">
                <div class="collapse collapse-arrow border border-base-300 bg-base-100 rounded-box mb-1" @click="collapseCheck('level1', index1)" >
                    <input type="checkbox" v-model="item.state"/>
                    <span class="collapse-title text-base font-medium text-sm  text-primary-600 leading-7 ">{{item.title}}</span>
                    <div class="collapse-content">
                        <template v-for="(item2, index2) in item.content" :key="index2">
                            <div class="collapse bg-base-100 rounded-box mb-1" @click="collapseCheck('level2', index1, index2)">
                                <input type="checkbox" v-model="item2.state"/>
                                <span class="collapse-title text-xl font-medium text-sm text-primary-500">{{item2.title}}</span>
                                <div class="collapse-content grid grid-cols-2 items-center text-center">
                                    <template v-for="(item3, index3) in item2.content" :key="index3">
                                        <span class="font-medium text-sm text-primary-500">{{item3.text}}</span>
                                        <select 
                                            class="select mb-1 input-bordered focus:outline-none"
                                            v-model="item3.value"
                                            @change="collapseOnChange(item3, index1, index2, index3, $event.target.value)"
                                        >
                                            <option v-if="index1" value="0"  >請選擇</option>
                                            <option value="1" >完全開放</option>
                                            <option value="4">完全不開放</option>
                                            <option value="3">逐次決定</option>
                                            <option value="2">選擇性開放</option>
                                        </select>
                                    </template>
                                </div>
                            </div>
                        </template>
                    </div>
                </div>
            </template>
            RMS已根據您對上列問題的答案為您設定資料開放偏好如下。您可以（a. 回到先前的問題，修改您的答案；b. 直接於下表中修改設定），以最符合您的資料開放偏好。
        </div>

        <div class="flex flex-col sm:flex-row sm:space-x-12 items-center">
            <UsageTable  class="sm:w-full" v-show="true" :dataContSubList="dataContSub"></UsageTable>
        </div>
        <div class="divider"></div>
        <div class="space-x-4 text-center">
            <!-- <router-link class="btn btn-outline w-40 mb-3" :to="{path: '/dataType', query: { setup: 'range',question:3}}" @click="TempContSub">上一步</router-link> -->
            <!-- <router-link class="btn btn-outline w-40 mb-3" :to="{path: '/'}" @click="TempContSub">返回</router-link> -->
            <label for="my-modal2" class="btn w-40 modal-button">返回</label>
            <label for="my-modal" class="btn w-40 modal-button">確定</label>
        </div>
        <!-- Put this part before </body> tag -->
        <input type="checkbox" id="my-modal" class="modal-toggle"/>
        <div class="modal">
            <div class="modal-box">
                <h3 class="font-bold text-lg">提示：</h3>
                <ul>
                    <li>是否確認儲存?</li>
                    <!--<li>不儲存設定並關閉頁面?</li>-->
                </ul>
                <div class="modal-action justify-center">
                <label for="my-modal" class="btn btn-outline w-40">返回</label>
                    <button class="btn w-40" @click="handleSave()">確定</button>
            <!--  <router-link class="btn w-40" to="/" >確定</router-link>-->
                </div>
            </div>
        </div>

        <!-- Put this part before </body> tag -->
        <input type="checkbox" id="my-modal2" class="modal-toggle"/>
        <div class="modal">
            <div class="modal-box">
                <h3 class="font-bold text-lg">提示：</h3>
                <ul>
                    <li>是否放棄儲存?</li>
                    <!--<li>不儲存設定並關閉頁面?</li>-->
                </ul>
                <div class="modal-action justify-center">
                <label for="my-modal2" class="btn btn-outline w-40">返回</label>
                    <router-link class="btn w-40" :to="{path: '/'}" @click="TempContSub">確定</router-link>
                </div>
            </div>
        </div>

        <!-- modal -->
        <input type="checkbox" id="collapse-modal" class="modal-toggle" v-model="showCheckMmodal[0]" />
        <div class="modal">
            <!-- <ChooseModal v-show="false" ></ChooseModal> -->
            <div class="modal-box max-w-4xl">
                <template v-if="showCheckMmodal[1]">
                    <h3 class="font-bold text-lg mb-4">
                        您選擇「選擇性開放」，意即：<span class=" text-red-500">{{ showCheckTitle }}</span>，僅開放與下列疾病或健康狀態有關者。（可複選）
                    </h3>
                </template>
                <template v-else>
                    <h3 class="font-bold text-lg mb-4">
                        您選擇「選擇性開放」，意即：<span class=" text-red-500">{{ showCheckTitle }}</span>的研究計畫應符合以下研究方向，方可利用您的資料。（可複選）
                    </h3>
                </template>
                <div class="checkedList space-y-2">
                    <div v-for="(item, index) in dataSub.value" :key="index">
                        <table class="table-fixed w-full">
                            <tr class="table-item">
                                <td><strong class="font-bold text-primary-600 text-left">{{ item.CNAME }}</strong></td>
                                <td>
                                    <label class="w-full sm:w-auto inline-flex tooltip-bottom space-x-2 tooltip sm:tooltip-right" :data-tip="item.CDESC">
                                        <input v-if="!showCheckMmodal[1]" type="checkbox" class="checkbox checkbox-primary" @change="funCheckSubSel(dataConSubOne, 1, showCheckMmodal[1], index)" 
                                            v-model="item.ANS" />
                                        <input v-else type="checkbox" class="checkbox checkbox-primary" @change="funCheckSubSel(dataConSubOne, 1, showCheckMmodal[1], index)" 
                                            v-model="item.ANS2" />
                                        <strong class="font-bold text-primary-600 text-left">同意</strong>
                                    </label>
                                </td>
                                <td>
                                    <label class="w-full sm:w-auto inline-flex space-x-2">
                                        <input type="checkbox" class="checkbox checkbox-primary" @change="funCheckSubSel(dataConSubOne, 2, showCheckMmodal[1], index)"
                                            :checked="!showCheckMmodal[1] ? !item.ANS : !item.ANS2" />
                                        <strong class="font-bold text-primary-600 text-left">不同意</strong>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-action flex justify-between items-center">
                    <div class=" space-x-4">
                        <label class="inline-flex" for="">
                            <input type="checkbox" class="checkbox checkbox-accent mr-2" v-model="subChkAll"
                                @click="funSubSelAllNot(true, showCheckMmodal[1])" /> 全選
                        </label>
                        <label class="inline-flex" for="">
                            <input type="checkbox" class="checkbox checkbox-accent mr-2" v-model="subChkNo"
                                @click="funSubSelAllNot(false, showCheckMmodal[1])" /> 全不選
                        </label>

                    </div>
                    <label for="collapse-modal" class="btn" @click="funCheckSubSel(dataConSubOne, false, showCheckMmodal[1])">確定</label>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import { useRouter } from 'vue-router'
    import { defineComponent, ref, onMounted, reactive} from 'vue';
    import { apihandler } from '../../api/apiHandler.js';
    import UsageTable from './components/UsageTable.vue';
    import ChooseModal from './components/ChooseModal.vue';

export default defineComponent({
    components: {UsageTable, ChooseModal},
    setup() {
        //----- 類
        const classRouter = useRouter();
        const apiHander = apihandler();
       

        //------- 資料屬性
        const showCheckMmodal = ref([false, true]); // [0]:modal show ; [1]: 步驟1(true)、步驟2(false)
        const showCheckTitle = ref(null);
        const dataConSubOne = reactive([]);
        const dataSub = reactive([]);
        const subChkAll = ref(false);
        const subChkNo = ref(false);
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
        const paraRowMapId = { "公共": "DC007", "非公共": "DC008", "商業": "DC009", "非商業": "DC010", "境內": "DC011", "跨境": "DC012" } // row 表 參數
        const paraColMapId = [["DC001"], ["DC002"], ["DC003"], ["DC004"], ["DC005"], ["DC006"]] // col 表對應參數
        const dataContSub = reactive([]);//題目與子項內容答案
        const reqData = reactive({
            TokenID: "",
            Token: "",
            Page: "",
            ContAnsList: []
        });
        const reqDataCheck = reactive({
            TokenID: "",
            Token: "",
            Page: ""
        })
        const usageList2 = ref([
            {
                title: "根據對方研究計畫變更偏好設定",
                state: false,
                content: [
                    {
                        title: "經費來源 + ",
                        state: false,
                        content: [
                            {
                                text: "公部門",
                                Id: paraRowMapId["公共"],
                                value: 1
                            },
                            {
                                text: "私部門",
                                Id: paraRowMapId["非公共"],
                                value: 1
                            }
                        ]
                    },
                    {
                        title: "研究目的 + ",
                        state: false,
                        content: [
                            {
                                text: "商業",
                                Id: paraRowMapId["商業"],
                                value: 1
                            },
                            {
                                text: "非商業",
                                Id: paraRowMapId["非商業"],
                                value: 1
                            }
                        ]
                    },
                    {
                        title: "跨境合作 + ",
                        state: false,
                        content: [
                            {
                                text: "限境內",
                                Id: paraRowMapId["境內"],
                                value: 1
                            },
                            {
                                text: "涉及跨境",
                                Id: paraRowMapId["跨境"],
                                value: 1
                            }
                        ]
                    }
                ]
            },
            {
                title: "根據我的資料變更偏好設定",
                state: false,
                content: [
                    {
                        title: "資料來源 + ",
                        state: false,
                        content: [
                            {
                                text: "診斷或治療",
                                Id: ["DC001", "DC002"],
                                value: 0
                            },
                            {
                                text: "健檢",
                                Id: ["DC003", "DC004"],
                                value: 0
                            },
                            {
                                text: "學術研究",
                                Id: ["DC005", "DC006"],
                                value: 0
                            }
                        ]
                    },
                    {
                        title: "資料形式 + ",
                        state: false,
                        content: [
                            {
                                text: "紀錄資料",
                                Id: ["DC001", "DC003", "DC005"],
                                value: 0
                            },
                            {
                                text: "檢體",
                                Id: ["DC002", "DC004", "DC006"],
                                value: 0
                            }
                        ]
                    }
                ]
            },
        ])

        //---------- 功能

        // collapse 自動開合
        const collapseCheck = (level, idx1, idx2) => {
            if (level == "level1") {
                if (usageList2.value[idx1 == 0 ? 1 : 0].state) {
                    usageList2.value[idx1 == 0 ? 1 : 0].state = false
                }
            }else if (level == "level2") {
                if (!usageList2.value[idx1].content[idx2].state) {
                    usageList2.value[idx1].content.forEach( item => {
                        if (item.state) {
                            item.state = false
                        }
                    })
                }
            }
        }

        // collapse select選項變更
        let usageList2Number = []
        const collapseOnChange = (info, idx1, idx2, idx3, value) => {
            usageList2Number = [info.Id, idx1, idx2, idx3]
            if (dataContSub.value != null) {
                if (idx1 == 0) {
                    const selContSub = dataContSub.value.filter(item => item.DC_ID == info.Id)[0]
                    usageList2.value[idx1].content[idx2].content[idx3].data = selContSub
                    usageList2.value[idx1].content[idx2].content[idx3].value = value
                }else {
                    const selContSub = []
                    info.Id.forEach( id => {
                        selContSub.push(dataContSub.value.filter(item => item.DC_ID == id)[0])
                    })
                    usageList2.value[idx1].content[idx2].content[idx3].data = selContSub
                }
                collapseInfo(info, idx1, idx2)
            }
        }

        // collapse select資料變更
        const collapseInfo = (info, idx1, idx2) => {
            // 請選擇
            if (info.value == 0)
                return
            
            // 選擇性開放
            if (info.value == 2) {
                dataConSubOne.value = info;
                dataSub.value = idx1 == 0 ? info.data.ANS_SUB : info.data[0].ANS_SUB;
                for(let i = 0; i < dataSub.value.length; i++) dataSub.value[i].ANS2 = true
                showCheckTitle.value = idx1 == 0 ?  selectTitle.value[info.data.DC_NUM] : idx2 == 0 ? selectTitle.value[info.data[0].DC_NUM] : "";
                showCheckMmodal.value = [true, idx1 == 0 ? false : true]; 
                subChkAll.value = idx1 == 0 ? false : true;
                subChkNo.value = false;
                if (idx1) {
                    funSubSelAllNot(true, true);
                }else {
                    funCheckSubSel(dataConSubOne, false, showCheckMmodal.value[1]);
                }
            }

            if (idx1 == 0) {
                dataContSub.value.forEach( item => {
                    if (item.DC_ID == info.Id) {
                        item.ANS = info.value
                    }
                })
            }else {
                info.Id.forEach( id => {
                    dataContSub.value.forEach( item => {
                        if (item.DC_ID == id) {
                            if (idx2 == 0) {
                                item.ANS = info.value
                            }else {
                                item.ANS2 = info.value
                            }
                        }
                    })
                })
            }
        }

        // 子選項 全選/全不選
        const funSubSelAllNot = (isSel, stepCheck) => {
            if (isSel) {
                subChkNo.value = false;
                if (!stepCheck) {
                    dataConSubOne.value.data.ANS_SUB.forEach(function (sub) {
                        sub.ANS = true;
                    });
                }else {
                    dataConSubOne.value.data.forEach( item => {
                        item.ANS_SUB.forEach(function (sub){
                            sub.ANS = true;
                            sub.ANS2 = true;
                        })
                    })
                }
            }
            else {
                subChkAll.value = false;
                if (!stepCheck) {
                    dataConSubOne.value.data.ANS_SUB.forEach(function (sub) {
                        sub.ANS = false;
                    });
                }else {
                    dataConSubOne.value.data.forEach( item => {
                        item.ANS_SUB.forEach(function (sub){
                            sub.ANS = false;
                            sub.ANS2 = false;
                        })
                    })
                }
            }
            funCheckSubSel(dataConSubOne, false, stepCheck);

            if (dataContSub.value != null && usageList2Number[1] == 0) {
                usageList2.value[0].content[usageList2Number[2]].content[usageList2Number[3]].value = isSel ? 1 : 4
            }
        };

        // 子項 判斷全選與否
        const funCheckSubSel = (inConSubOne, unAgree, stepCheck, index) => {
            if (inConSubOne == null || inConSubOne.value.data.length == 0)
                return;

            // 1(同意)、2(不同意)
            if (unAgree == 1) {
                console.log(dataSub.value)
                if (stepCheck) {
                    dataSub.value[index].ANS = dataSub.value[index].ANS2;
                    inConSubOne.value.data.forEach( item => {
                        item.ANS_SUB[index].ANS = dataSub.value[index].ANS
                    })
                }
            }else if (unAgree == 2) {
                if (!stepCheck) {
                    inConSubOne.value.data.ANS_SUB[index].ANS = !inConSubOne.value.data.ANS_SUB[index].ANS;
                }else {
                    dataSub.value[index].ANS2 = !dataSub.value[index].ANS2
                    inConSubOne.value.data.forEach( item => {
                        item.ANS_SUB[index].ANS = !item.ANS_SUB[index].ANS
                    })
                }
            }

            // stepCheck -> 步驟1(true)、步驟2(false)
            if (!stepCheck) {
                const chkSubNum = inConSubOne.value.data.ANS_SUB.filter(item => item.ANS).length;
                if (chkSubNum == 0) {
                    subChkAll.value = false;
                    subChkNo.value = true;
                    inConSubOne.value.data.ANS = 4;
                }
                else if (chkSubNum == inConSubOne.value.data.ANS_SUB.length) {
                    subChkAll.value = true;
                    subChkNo.value = false;
                    inConSubOne.value.data.ANS = 1;
                }
                else {
                    subChkAll.value = false;
                    subChkNo.value = false;
                    inConSubOne.value.data.ANS = 2;
                }
            }else {
                inConSubOne.value.data.forEach( item => {
                    const chkSubNum = item.ANS_SUB.filter(item => item.ANS).length;
                    if (chkSubNum == 0) {
                        subChkAll.value = false;
                        subChkNo.value = true;
                        item.ANS = 4;
                        inConSubOne.value.value = 4;
                    }
                    else if (chkSubNum == item.ANS_SUB.length) {
                        subChkAll.value = true;
                        subChkNo.value = false;
                        item.ANS = 1;
                        inConSubOne.value.value = 1;
                    }
                    else {
                        subChkAll.value = false;
                        subChkNo.value = false;
                        item.ANS = 2;
                        inConSubOne.value.value = 2;
                    }
                })
            }
        };

        // 儲存ContSub 至session
        const TempContSub = () => {
            sessionStorage.setItem("ConSub", JSON.stringify(dataContSub.value));
        }

        // 按鈕 儲存
        const handleSave = () => {
            reqData.ContAnsList = dataContSub.value;

            let ConSubOther = sessionStorage.getItem("ConSubOther")
            if(ConSubOther == undefined || ConSubOther == null){
                ConSubOther = []
                sessionStorage.setItem("ConSubOther",JSON.stringify(ConSubOther));
            }else{
                ConSubOther = JSON.parse(ConSubOther)
            }
            let arrayTemp = []
            for(let i in ConSubOther){
                let data = JSON.parse(sessionStorage.getItem(ConSubOther[i]))
                let arrayTemp2 = []
                for(let j in data){
                    let sub = []
                    if(data[j].ANS_SUB_OPTIONS != undefined && data[j].ANS_SUB_OPTIONS != null){
                        for(let k in data[j].ANS_SUB_OPTIONS){
                            sub.push(data[j].ANS_SUB_OPTIONS[k].DS_ID)
                        }
                    }

                    arrayTemp2.push({
                        dcId:data[j].dcId,
                        ANS:data[j].ANS,
                        sub:sub,
                    })
                }

                arrayTemp.push({name:ConSubOther[i],list:arrayTemp2})
            }
            reqData.ContAnsOtherList = arrayTemp
            // console.log(reqData)
            apiHander.FunctionToken(checkPrivated, reqDataCheck);
            //apiHander.FunctionToken(addContAns, reqData);
        }

        // 確認是否已登入
        const userSignCheck = async () => {
            if (sessionStorage.getItem("TokenID") === null) {
                alert("請登入系統!!");
                classRouter.push({ path: "/Login" });
            }
            else {
                //apiHander.FunctionToken(checkPrivated, reqDataCheck);
                apiHander.FunctionToken(getPriCont, reqData);
                if(classRouter.currentRoute.value.query.from == 'unit'){
                    classRouter.replace({'query':{}})
                }else{
                    apiHander.FunctionToken(getPriContOther, reqData);
                }
            }
        }

        //-------api---------
        const apiCheckPrivated = (data) => apiHander.userRequest.post("api/PrivacyPre/CheckPrivated", data);
        const apiAddContAns = (data) => apiHander.userRequest.post("api/PrivacyPre/AddContAns", data);
        const apiUpdateContAn = (data) => apiHander.userRequest.post("api/PrivacyPre/UpdateContAns", data);
        const apiGetPriCont = (data) => apiHander.userRequest.post("api/PrivacyPre/GetPriCont", data);
        const apiGetPriContOther = (data) => apiHander.userRequest.post("api/PrivacyPre/GetPriContOther", data);

        
        const addContAns = (data) => {
            apiAddContAns(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        alert("儲存成功");
                        sessionStorage.removeItem("ConSub");                        
                        classRouter.push({
                            path: '/'
                        });
                    } else {
                        alert(`儲存失敗，${json.Data}`);
                    }
                })
                .catch((err) => {
                    alert(`儲存失敗，${err}`);
                    console.log(err);
                });
        }
        const updateContAns = (data) => {
            apiUpdateContAn(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        alert("儲存成功");
                        sessionStorage.removeItem("ConSub");
                        classRouter.push({
                            path: '/'
                        });
                    } else {
                        alert(`儲存失敗，${json.Data}`);
                    }
                })
                .catch((err) => {
                    alert(`儲存失敗，${err}`);
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
                           // classRouter.push({ path: "/" });
                            apiHander.FunctionToken(updateContAns, reqData);
                        }
                        else {
                            //dataContSub.value = JSON.parse(sessionStorage.getItem("ConSub"));
                            apiHander.FunctionToken(addContAns, reqData);
                        }
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        } 

        //取得題目答案
        const getPriCont = (data) => {
            apiGetPriCont(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        sessionStorage.setItem("ConSub", json.Data);
                        dataContSub.value = JSON.parse(json.Data);
                        //帶入預設值 console.log('here',dataContSub.value)
                        dataContSub.value.map(item => {
                            usageList2.value[0].content.map(x => x.content.filter(y => y.Id == item.DC_ID)).map(x => {if(x[0]) x[0].value = item.ANS})
                        })
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
        //------------週期
        onMounted(() => {
            userSignCheck()
        })
        return {
            dataContSub,
            usageList2,
            paraRowMapId,
            paraColMapId,
            TempContSub,
            handleSave,
            collapseCheck,
            collapseInfo,
            collapseOnChange,
            showCheckMmodal,
            showCheckTitle,
            dataSub,
            subChkAll,
            subChkNo,
            funSubSelAllNot,
            funCheckSubSel,
            dataConSubOne,
            getPriCont,
            getPriContOther,
        }
    },
    
})
</script>