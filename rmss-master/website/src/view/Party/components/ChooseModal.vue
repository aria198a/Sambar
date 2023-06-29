<template>
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
</template>

<script>
    import { useRouter } from 'vue-router'
    import { defineComponent, ref, reactive } from 'vue';

export default defineComponent({


    setup() {
    //----- 類
        const classRouter = useRouter();

    //------- 資料屬性
        const dataConSubOne = reactive([]);
        const dataSub = reactive([]);
        const subChkAll = ref(false);
        const subChkNo = ref(false);
        const showCheckTitle = ref(null);
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


        
    //---------- 功能
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



        return {
            funSubSelAllNot,
            funCheckSubSel
        }
    }
})
</script>