<template>
    <div class="container mx-auto px-6 pb-12 sm:px-0">
        <div class="text-sm breadcrumbs">
           
            <ul>
              <li><a @click="toHome">RMS</a></li>
              <li><a @click="toHome">增修計畫</a></li>
              <li>再利用類型</li>
            </ul>
        </div>
        <h1 class="step-title">再利用類型</h1>
        <div class="radio-box">
            <div class="grid grid-cols-12 grid-rows-8 grid-table">
                <div class="col-span-10 th">資料再利用脈絡與研究目標：</div>
                <div class="th mid">是</div>
                <div class="th mid">否</div>
                <div class="mid !justify-start row-span-16 text-center">再利用脈絡</div>
                <div class="mid !justify-start col-span-2 row-span-6">健康資料來源</div>
                <template v-for="(item, index) in usageA" :key="index"> 
                    <div class="col-span-2 row-span-2 p-0">{{ item.title }}</div>
                    <div v-if="index == 0" class="mid !justify-start col-span-2 row-span-6">健康資料利用類型</div>
                    <div class="col-span-3">資料紀錄</div>
                    <div class="radioBlock"><input type="radio" :name="'record-'+index" class="radio" value="1" v-model="planDetail.ApX[item.rKey]" /></div>
                    <div class="radioBlock"><input type="radio" :name="'record-'+index" class="radio" value="2" v-model="planDetail.ApX[item.rKey]" /></div>
                    <div class="col-span-3">檢體</div>
                    <div class="radioBlock"><input type="radio" :name="'specimen-'+index" class="radio" value="1" v-model="planDetail.ApX[item.sKey]" /></div>
                    <div class="radioBlock"><input type="radio" :name="'specimen-'+index" class="radio" value="2" v-model="planDetail.ApX[item.sKey]" /></div>
                </template>
                <div class="mid col-span-2 row-span-10">疾病及健康狀況 disease & health condition (ICD-10)</div>
                <template v-for="(item, index) in usageB" :key="index">
                    <div class="col-span-7">{{item.title}}</div>
                    <div class="radioBlock"><input type="radio" :name="'disease-'+index" class="radio" value="1" v-model="planDetail.ApXS[item.key]" /></div>
                    <div class="radioBlock"><input type="radio" :name="'disease-'+index" class="radio" value="2" v-model="planDetail.ApXS[item.key]" /></div>
                </template>
            </div>
        </div>
        <div class="text-center space-x-2 mt-3">
            <router-link class="btn btn-outline w-40" to="/planContext" @click="saveDetail()">上一步</router-link>
            <label for="my-modal" class="btn w-40 modal-button" >送出申請</label>
        </div>
      <ul class="steps grid mt-12">
        <li class="step step-primary">計畫基本資料</li>
        <li class="step step-primary">再利用脈絡</li>
        <li class="step step-primary">再利用類型</li>
      </ul>
    </div>
    <input type="checkbox" id="my-modal" class="modal-toggle" />
    <div class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">送出申請</h3>
            <p class="py-4">您確定要送出申請</p>
            <div class="modal-action justify-center">
                <!--<router-link class="btn w-40" @click="saveOnline">確定</router-link>-->
                <label for="my-modal" class="btn btn-outline w-40">返回</label>
                <label for="my-modal" class="btn  w-40" @click="saveOnline()">確定</label>
            </div>
        </div>
    </div>
  
</template>
<script>
 
    import { defineComponent, reactive, ref, onMounted, onBeforeMount } from 'vue';
    import { useRouter } from 'vue-router';
    import { useRoute } from 'vue-router';
    import { apihandler } from '../../api/apiHandler.js';
export default defineComponent({
    setup() {        
        const router = useRouter();
        const apiHander = apihandler();
        const route = useRoute();

        //===== model
        const usageA = reactive([
            {
                title: '現有診斷治療',
                record: null,
                specimen: null,
                rKey: "DT_RECORD",
                sKey:"DT_SPECIMEN"
            },
            {
                title: '預防健檢',
                record: null,
                specimen: null,
                rKey: "HE_RECORD",
                sKey: "HE_SPECIMEN"
            },
            {
                title: '參與學術研究',
                record: null,
                specimen: null,
                rKey: "AR_RECORD",
                sKey: "AR_SPECIMEN"
            },
        ])
        const usageB = reactive([
            {
                title: '癌症（C00-D48）',
                key:"CANCER"
            },
            {
                title: '心血管疾病（D50-D89',
                key: "CARDIO"
            },
            {
                title: '糖尿病（E00-E90）',
                key: "DIABETES"
            },
            {
                title: '神經退化性疾病（G00-G99',
                key: "NEURO"
            },
            {
                title: '心理健康與精神疾病（F00-F99）',
                key: "MENTAL"
            },
            {
                title: '產期與生殖健康（NOO-N99, O00-O99, P00-P96, Q00-Q99）',
                key: "BIRTH"
            },
            {
                title: '傳染性疾病（A00-B99）',
                key: "INFECTIOUS"
            },
            {
                title: '耳及乳突之疾病（G60-G95）',
                key: "EAR"
            },
            {
                title: '小兒科（N/A）',
                key: "PEDIATRICS"
            },
            {
                title: '長者與老年醫學（N/A）',
                key: "ELDERLY"
            },
        ])
        const planDetail = ref({           
        });

        //=== 事件
        const saveDetail = function () {
            apiHander._sessionData("planDetail", JSON.stringify(planDetail.value));
        };
        const removeDetail = function () {
            apiHander._sessionData("planDetail", "");
            
        };
        const saveOnline = function () {
          let isAllWrite=1;
          usageA.forEach(r=>{
            if(planDetail.value.ApX[r.rKey]===0 || planDetail.value.ApX[r.sKey]===0){
              isAllWrite= 0;
            }
          })
          usageB.forEach(r=>{
            if(planDetail.value.ApXS[r.key]===0){
              isAllWrite= 0;
            }
          })
          // all set need to setting
          if(isAllWrite===1) {
            if (planDetail.value.Main.APM_ID != null && planDetail.value.Main.APM_ID != "") {
              apiHander.FunctionToken(updateApplyInfo, planDetail.value);
            } else
              apiHander.FunctionToken(addApplyInfo, planDetail.value);
          }
          else
            alert('尚有資料未填! 請確認填寫後至下一頁');
        };

        const toHome = ()=>{
          let confirm = window.confirm("資料尚未儲存，是否確認離開？");
          if(confirm)
            router.push({path:'/'});
        };
        ///===== api        
        const apiAddApplyInfo = (data) => apiHander.userRequest.post("api/Apply/AddApplyInfo", data);
        const addApplyInfo = (data) => {
            apiAddApplyInfo(data)
                .then((res) => {
                    const json = res.data;
                    console.log(json)
                    if (json.Status) {
                        alert("新增成功");
                        removeDetail();
                        router.push({ path: "/" });
                    }
                    else
                        alert("新增失敗：" + json.Message);
                })
                .catch((err) => {
                    console.log(err);
                });
        }

        const apiUpdateApplyInfo = (data) => apiHander.userRequest.post("api/Apply/UpdateApplyInfo", data);
        const updateApplyInfo = (data) => {
            apiUpdateApplyInfo(data)
                .then((res) => {
                    const json = res.data;
                    if (json.Status) {
                        alert("更新成功");
                        removeDetail();
                        router.push({ path: "/" });
                    
                    }
                    else
                        alert("更新失敗："+json.Message);
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
            else if (sessionStorage.getItem("planDetail") != null) {
                planDetail.value = JSON.parse(sessionStorage.getItem("planDetail"));
            }
            else {
                router.push({ path: "/PlanInfo" });
            }
        })

        // ======= public      
        return {
            usageA,
            usageB,
            saveDetail,
            planDetail,
            saveOnline,
            toHome
        }
    }
})
</script>
