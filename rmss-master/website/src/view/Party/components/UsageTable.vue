<template>
    <div class="flex flex-col sm:flex-row sm:space-x-12 items-center">
        <div class="sm:w-full">
            <div class="radio-box">
                <div class="grid-table grid grid-flow-row-dense grid-cols-9 grid-rows-3 text-center">
                    <div class="col-span-3 th mid">-</div>
                    <div class="col-span-6 th mid">開放資料類型</div>
                    <div class="col-span-3 th mid">再利用類型</div>
                    <div class="col-span-2 th mid">診斷或治療疾病</div>
                    <div class="col-span-2 th mid">預防疾病</div>
                    <div class="col-span-2 th mid">參與學術研究</div>
                    <div class="th mid">經費來源</div>
                    <div class="th mid">預期目的</div>
                    <div class="th mid">跨境合作</div>
                    <div class="col-span-1 th mid">紀錄資料</div>
                    <div class="col-span-1 th mid">檢體</div>
                    <div class="col-span-1 th mid">紀錄資料</div>
                    <div class="col-span-1 th mid">檢體</div>
                    <div class="col-span-1 th mid">紀錄資料</div>
                    <div class="col-span-1 th mid">檢體</div>
                </div>
                <div class="grid-table grid grid-flow-row-dense grid-cols-9 grid-rows-8 mb-5">
                    <template v-for="(levelOne , index1) in usageList" :key="index1">
                        <div class="row-span-4 mid justify-center">{{levelOne.title}}</div>
                        <template v-for="(levelTwo, index2) in levelOne.content" :key="index2">
                            <div class="row-span-2 mid justify-center">{{levelTwo.title}}</div>
                            <template v-for="(levelThree, index3) in levelTwo.content" :key="index3">
                                <div class="mid justify-center">{{levelThree.title}}</div>
                                <template v-for="(levelFour, index4) in levelThree.content" :key="index4">
                                    <div
                                        @click="handleGoContSub(paraRowMapId[levelOne.title],paraRowMapId[levelTwo.title],paraRowMapId[levelThree.title],paraColMapId[index4])"
                                        class="col-span-1 text-center"
                                        :class="funGetColor(paraRowMapId[levelOne.title],paraRowMapId[levelTwo.title],paraRowMapId[levelThree.title],paraColMapId[index4])"
                                    >
                                        <template v-if="funGetPurDataInfo(paraRowMapId[levelOne.title],paraRowMapId[levelTwo.title],paraRowMapId[levelThree.title],paraColMapId[index4])">
                                            <div 
                                                class="tooltip-top space-x-2 tooltip sm:tooltip-top whitespace-pre-line"
                                                :data-tip="funGetPurDataInfo(paraRowMapId[levelOne.title],paraRowMapId[levelTwo.title],paraRowMapId[levelThree.title],paraColMapId[index4])"
                                            >
                                                <img src="../../../assets/image/datausage01.png" alt="">
                                            </div>
                                        </template>
                                    </div>
                                </template>
                            </template>
                        </template>
                    </template>
                </div>
            </div>
            <ul class="flex flex-col sm:flex-row sm:justify-center space-y-2 sm:space-y-0 sm:space-x-4">
                <li class="flex items-center"><span class="inline-block aspect-square border bg-green-100 border-green-500 w-6 mr-1"></span>完全開放</li>
                <li class="flex items-center"><span class="inline-block aspect-square border bg-gray-100 border-gray-500 w-6 mr-1"></span>完全不開放</li>
                <li class="flex items-center"><span class="inline-block aspect-square border bg-blue-100 border-blue-500 w-6 mr-1"></span>逐次決定</li>
                <li class="flex items-center"><span class="inline-block aspect-square border bg-yellow-100 border-yellow-500 w-6 mr-1"></span>選擇性開放</li>
            </ul>
        </div>
    </div>
</template>

<script>
    import { useRouter } from 'vue-router'
    import { defineComponent, ref, onMounted, reactive } from 'vue';

export default defineComponent({
    props: ["dataContSubList"],
    setup(props) {
        //----- 類
        const classRouter = useRouter();

        //------- 資料屬性
        const paraRowMapId = { "公共": "DC007", "非公共": "DC008", "商業": "DC009", "非商業": "DC010", "境內": "DC011", "跨境": "DC012" } // row 表 參數
        const paraColMapId = [["DC001"], ["DC002"], ["DC003"], ["DC004"], ["DC005"], ["DC006"]] // col 表對應參數
        const dataContSub = props.dataContSubList;
        // const dataContSub = reactive([]);//題目與子項內容答案
        const usageList = ref([
        {
            title:"公共",
            content: [
                {
                    title: '商業',
                    content: [
                        {
                        title: '境內',
                            content: [
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },{
                                    state: 0,
                                    describe:null
                                }
                            ]
                        },
                        {
                            title: '跨境',
                            content: [
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },{
                                    state: 0,
                                    describe:null
                                }
                            ]
                        }
                    ]
                },
                {
                    title: '非商業',
                    content: [
                        {
                        title: '境內',
                            content: [
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },{
                                    state: 0,
                                    describe:null
                                }
                            ]
                        },
                        {
                            title: '跨境',
                            content: [
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                    
                                }
                            ]
                        }
                    ]
                }
            ]
        },
        {
            title:"非公共",
            content: [
                {
                    title: '商業',
                    content: [
                        {
                        title: '境內',
                            content: [
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                }
                            ]
                        },
                        {
                            title: '跨境',
                            content: [
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                }
                            ]
                        }
                    ]
                },
                {
                    title: '非商業',
                    content: [
                        {
                        title: '境內',
                            content: [
                                {
                                    state: 1,
                                    describe:null
                                },
                                {
                                    state: 1,
                                    describe:null
                                },
                                {
                                    state: 1,
                                    describe:null
                                },
                                {
                                    state: 1,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                }
                            ]
                        },
                        {
                            title: '跨境',
                            content: [
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                },
                                {
                                    state: 0,
                                    describe:null
                                }
                            ]
                        }
                    ]
                }
            ]
        }
    ])


        //
        const handleGoContSub = (rdcIds1, rdcIds2, rdcIds3, cdcIds) => {
            let dcidsStr = `${cdcIds.join()},${rdcIds1},${rdcIds2},${rdcIds3}`;
            console.log(`dcidsStr is ${dcidsStr}`);
            // const selContSub = dataContSub.value.filter(item => item.DC_ID == rdcIds1 || item.DC_ID == rdcIds2 || item.DC_ID == rdcIds3 || cdcIds.includes(item.DC_ID))
            // console.log(selContSub)
            console.log(funGetPurDataInfo(rdcIds1, rdcIds2, rdcIds3, cdcIds))

            classRouter.push({
                path: 'dataType',
                query: { setup: 'unit', dcids: dcidsStr }
            });
        }

        // 計算顏色 
        let PurDataInfoCheck = false // 防止 逐次決定&選擇性開放 覆蓋
        const funGetColor = (rdcIds1,rdcIds2,rdcIds3, cdcIds) => {
            let dcidsStr = `${cdcIds.join()},${rdcIds1},${rdcIds2},${rdcIds3}`;
            if (sessionStorage.getItem(dcidsStr) !=  null) {
                const list = JSON.parse(sessionStorage.getItem(dcidsStr))
                let number = 0;
                list.map( item => {
                    if (item.dcId == rdcIds1 || item.dcId == rdcIds2 || item.dcId == rdcIds3 || item.dcId == cdcIds) {
                        number += 1
                    }
                })
                if (number == 4) {
                    // console.log(list)
                    const selAns = list.map(item =>{ return item.ANS });

                    if (selAns.length > 0) {
                        if ((Math.max(...selAns) == 2 || Math.max(...selAns) == 3) && !selAns.includes("4")) {
                            PurDataInfoCheck = true;
                        }
                            
                        switch (Math.max(...selAns)) {
                            case 1: return "bg-green-100 hover:bg-green-200";
                            case 2: return "bg-yellow-100 hover:bg-yellow-200";
                            case 3: return "bg-blue-100 hover:bg-blue-200";
                            case 4: return "bg-gray-100 hover:bg-gray-200";
                        }
                    }
                    else {
                        return "bg-gray-100 hover:bg-gray-200";
                    }
                }
            }

            if (dataContSub.value == null)
                return "bg-gray-100 hover:bg-gray-200";

            const selContSub = dataContSub.value.filter(item => item.DC_ID == rdcIds1 || item.DC_ID == rdcIds2 || item.DC_ID == rdcIds3 || cdcIds.includes(item.DC_ID))   
            const selAns = selContSub.map(item => {
                if (item.ANS2) {
                    if (item.ANS < item.ANS2 || item.ANS == 4) {
                        item.ANS = item.ANS2
                    }
                }
                return item.ANS
            });

            if (selAns.length > 0) {
                if ((Math.max(...selAns) == 2 || Math.max(...selAns) == 3) && !selAns.includes("4")) {
                    PurDataInfoCheck = true;
                }
                switch (Math.max(...selAns)) {
                    case 1: return "bg-green-100 hover:bg-green-200";
                    case 2: return "bg-yellow-100 hover:bg-yellow-200";
                    case 3: return "bg-blue-100 hover:bg-blue-200";
                    case 4: return "bg-gray-100 hover:bg-gray-200";
                }
            }
            else {
                return "bg-gray-100 hover:bg-gray-200";
            }
        }

        
        // 取得 目的、資料 內容
        const funGetPurDataInfo = (rdcIds1, rdcIds2, rdcIds3, cdcIds) => {
            // console.log(rdcIds1, rdcIds2, rdcIds3, cdcIds)
            let DMNumbler = [{ title: '目的:' , value: 'DM02'}, { title: '資料:' , value: 'DM01'}]
            if (dataContSub.value == null)
                return false;

            if (!PurDataInfoCheck) {
                PurDataInfoCheck = false;
                return false;
            }

            let dcidsStr = `${cdcIds.join()},${rdcIds1},${rdcIds2},${rdcIds3}`;
            // console.log(sessionStorage.getItem(dcidsStr))
            if (sessionStorage.getItem(dcidsStr) !=  null) {
                const colorNumber = funGetColor(rdcIds1, rdcIds2, rdcIds3, cdcIds)
                if (colorNumber.includes("gray") || colorNumber.includes("green")) {
                    return false;
                }

                const list = JSON.parse(sessionStorage.getItem(dcidsStr))
                let number = 0;

                list.map( item => {
                    if (item.dcId == rdcIds1 || item.dcId == rdcIds2 || item.dcId == rdcIds3 || item.dcId == cdcIds) {
                        number += 1
                    }
                })

                if (number == 4) {
                    let listANS = list.filter(item => item.ANS == 2)
                    let backInfo = ''
                    DMNumbler.map( (DM, idx) => {
                        listANS.map(item => {
                            if (DM.value == item.Name) {
                                idx ? backInfo += `\n ${DM.title}` : backInfo = DM.title
                            }
                            backInfo += item.ANS_SUB
                        })
                    })

                    // console.log(list)
                    // console.log(backInfo)
                    return backInfo
                }else {
                    return
                }
            }

            let backInfo = ''
            const colorNumber = funGetColor(rdcIds1, rdcIds2, rdcIds3, cdcIds)
            DMNumbler.forEach( DM => {
                const dsNameArr = [];
                let TrueNumber = false;
                const csArr = dataContSub.value.filter(item => (item.ANS==2 || item.ANS==3) && item.DC_MAIN == DM.value && (item.DC_ID == rdcIds1 || item.DC_ID == rdcIds2 || item.DC_ID == rdcIds3 || cdcIds.includes(item.DC_ID)));
                
                csArr.forEach( item => {
                    let countTrue = item.ANS_SUB.filter(item => item.ANS == true)
                    if (countTrue.length > 0 && countTrue.length != csArr[0].ANS_SUB.length) {
                        TrueNumber = true;
                    }
                })

                if (csArr.length > 0) {
                    if (TrueNumber) {
                        TrueNumber = false;
                        csArr.forEach(function (dc) {
                            let sNameArr = dc.ANS_SUB.filter(item => item.ANS == true).map(sub => sub.CNAME);
                            if (sNameArr.length > 0) {
                                sNameArr.forEach(function (s) {
                                    dsNameArr.push(s);   
                                })                                   
                            }
                        });
                    }
                }
                if (dsNameArr.length > 0 && !colorNumber.includes("gray")) {
                    const resultArr = dsNameArr.filter(onlyUnique);
                    backInfo += DM.title + resultArr.join() + '\n'
                }
                else
                    return false;
            })
            return backInfo;
        }

        // distinct
        const onlyUnique = (value, index, self) => {
            return self.indexOf(value) === index;
        }

        //------------週期
        onMounted(() => {
            
        })
        return {
            paraRowMapId,
            paraColMapId,
            usageList,
            handleGoContSub,
            funGetColor,
            funGetPurDataInfo,
        }
    }
})
    
</script>
