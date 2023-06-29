<template>
    <div class="container mx-auto px-6 pb-12 sm:px-0">
        <div class="text-sm breadcrumbs">
            <ul>
              <li><a @click="toHome">RMS</a></li>
              <li><a @click="toHome">增修計畫</a></li>
              <li>再利用脈絡</li>
            </ul>
        </div>
        <h1 class="step-title">再利用脈絡</h1>       
        <div class="radio-box">
            <div class="grid-table grid grid-cols-12 grid-rows-8">
                <div class="col-span-10 th">資料再利用脈絡與研究目標：</div>
                <div class="th mid justify-center">是</div>
                <div class="th mid justify-center">否</div>
                <div class="row-span-13 mid">再利用脈絡</div>
                <template v-for="(iteml1, idxl1) in contextQ" :key="idxl1">
                    <div class="col-span-3 mid !justify-start" :class="'row-span-'+iteml1.quations.length" :style="['grid-row: span ' + iteml1.quations.length + ' / span ' + iteml1.quations.length]" v-html="iteml1.title"></div>
                    <template v-for="(iteml2, idxl2) in iteml1.quations" :key="idxl2">
                        <div class="col-span-6">{{iteml2.quation}}</div>
                        <template v-if="idxl1 == 0">
                            <div class="radioBlock"><input type="radio" :name="'radio-' + idxl1 + idxl2" class="radio" value="1" v-model="planDetail.ApYS[iteml2.key]" /></div>
                            <div class="radioBlock"><input type="radio" :name="'radio-' + idxl1 + idxl2" class="radio" value="2" v-model="planDetail.ApYS[iteml2.key]" /></div>
                        </template>
                        <template v-else>
                            <div class="radioBlock"><input type="radio" :name="'radio-' + idxl1 + idxl2" class="radio" value="1" v-model="planDetail.ApY[iteml2.key]" @click="apYchange(iteml2.key,1)"/></div>
                            <div class="radioBlock"><input type="radio" :name="'radio-' + idxl1 + idxl2" class="radio" value="2" v-model="planDetail.ApY[iteml2.key]" @click="apYchange(iteml2.key,2)"/></div>
                        </template>
                    </template>
                </template>
            </div>
        </div>
        <div class="text-center space-x-2 mt-3">
          <!-- <router-link class="btn btn-outline w-40" to="/PlanInfo" @click="saveDetail(0)">上一步</router-link>-->
            <!--<router-link class="btn w-40" to="/planUsage" @click="saveDetail()">下一步</router-link>-->
          <button class="btn w-40" @click="saveDetail(0)">上一步</button>
          <button class="btn w-40" @click="saveDetail(1)">下一步</button>
        </div>
      <ul class="steps grid mt-12">
        <li class="step step-primary">計畫基本資料</li>
        <li class="step step-primary">再利用脈絡</li>
        <li class="step ">再利用類型</li>
      </ul>
    </div>
</template>
<script>
    import { defineComponent, reactive, ref, onMounted, onBeforeMount } from 'vue';
    import { useRouter } from 'vue-router';
    import { useRoute } from 'vue-router';
    import { apihandler } from '../../api/apiHandler.js'
    
    export default defineComponent({
        setup() {
            const router = useRouter();
            const apiHander = apihandler();
            const route = useRoute();

            //===== model
            const contextQ = reactive([
                {
                    title: 'purpose dimensions<br />研究目標',
                    quations: [
                        {
                            quation: '診斷',
                            key: 'DIAGNOSIS'
                        },
                        {
                            quation: '預防與治療'
                            ,
                            key: 'PREVENTION'
                        },
                        {
                            quation: '病患照護',
                            key: 'CARE'
                        },
                        {
                            quation: '病患安全',
                            key: 'SAFETY'
                        },
                        {
                            quation: '醫療機構的組織與架構',
                            key: 'TRUCTURE'
                        },
                        {
                            quation: '公共衛生政策',
                            key: 'POLICY'
                        },
                        {
                            quation: '疾病研究',
                            key: 'RESEARCH'
                        },
                    ]
                },
                {
                    title: '公共 / 非公共<br />商業 / 非商業<br />國內 / 非商業<br />',
                    quations: [
                        {
                            quation: '是否為本國公部門補助',
                            key: 'PUBLICINFO'
                        },
                        {
                            quation: '是否為有商業目的',
                            key: 'BUSINESS'
                        },
                        {
                            quation: '是否為未涉及跨境之合作計畫',
                            key: 'NFOREIGNINFO'
                        },
                    ]
                }
            ])
            const planDetail = ref({
                //ApYS: {A:0,B:0},
                //ApY: { A: 0, B: 0 }
            });

            //=== 事件
            const saveDetail = function (page) {
                let isAllWrite=1;
                if (page != 0) {
                    contextQ[0].quations.forEach( r =>{                
                        if( planDetail.value.ApYS[r.key] === 0 ){
                            isAllWrite= 0;
                        }
                    })
                    contextQ[1].quations.forEach(r=>{
                        if(planDetail.value.ApY[r.key]===0){
                            isAllWrite= 0;
                        }
                    })
                }
                // all set need to setting
                if(isAllWrite===1) {
                    apiHander._sessionData("planDetail", JSON.stringify(planDetail.value));
                    if (page === 0)
                        router.push({path: '/PlanInfo'});
                    else if (page === 1)
                        router.push({path: '/planUsage'});
                }
                else
                    alert('尚有資料未填! 請確認填寫後至下一頁');
            };

            const apYchange = (key,changeAns) => {
                switch (key) {
                    case "PUBLICINFO":
                        planDetail.value.ApY.NPUBLICINFO = changeAns == 1 ? 2 : 1;
                        break;
                    case "BUSINESS":
                        planDetail.value.ApY.EDU = changeAns == 1 ? 2 : 1;
                        break;
                    case "NFOREIGNINFO":
                        planDetail.value.ApY.FOREIGNINFO = changeAns == 1 ? 2 : 1;
                        break;
                }
            };
            
            const toHome = ()=>{
              let confirm = window.confirm("資料尚未儲存，是否確認離開？");
              if(confirm)
                router.push({path:'/'});
            };

            //=== 初始化
            onBeforeMount(() => {
                if (sessionStorage.getItem("TokenID") === null) {
                    alert("請登入系統!!");
                    route.push({ path: "/Login" });
                }
                else if (sessionStorage.getItem("planDetail") != null) {
                    planDetail.value = JSON.parse(sessionStorage.getItem("planDetail"));
                }
                else {
                    route.push({ path: "/PlanInfo" });
                }
            })

            // ======= public
            return {
                contextQ,
                saveDetail,
                planDetail,
                apYchange,
                toHome
            }
        }
    })
</script>