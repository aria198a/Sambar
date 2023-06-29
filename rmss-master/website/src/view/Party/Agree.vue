<template>
    <div class="agree">
        <div class="max-w-xl space-y-8">
            <h1 class="step-title">
                <!-- 偏好設定說明 -->
                RMS的目的
            </h1>
            <div class="space-y-4 bg-white p-8 border rounded-xl ">
                <!-- <p>接下來，RMS會協助您就「資料類型」與「再利用類型」完成偏好設定。</p>
                <p>每個問題有四個選項，對應「全部開放」、「逐次決定」、「選擇性開放」及「完全不開放」四種不同的資料開放程度。其個別意義如下：</p>
                <ul class="list-disc pl-8">
                    <li><strong>完全開放</strong>：代表您同意完全開放您的資料被再利用。</li>
                    <li><strong>逐次決定</strong>：代表平台應逐次詢問您的開放意願。</li>
                    <li><strong>選擇性開放</strong>：代表在符合特殊目的或情境下，您同意開放特定性質的資料被再利用。</li>
                    <li><strong>完全不開放</strong>：代表您不同意您的資料被再利用。</li>
                </ul>
                <p>請留意：</p>
                <ul class="list-disc pl-8">
                    <li>此處所指的資料，不僅限於既有已存的資料，也包含未來可能產生的資料。</li>
                    <li>再利用類型，是同時考量研究計畫的「經費來源」、「預期目的」與是否涉及「跨境合作」等多個因子的整體綜合評估。也就是說：您對第肆部分任一問題的答案，可能會因為您對第肆部份其他問題的答案而有所限縮。</li>
                    <li>所有您同意開放的資料都會經過去識別處理後，方才提供給申請者。</li>
                    <li>您可以隨時撤回或變更偏好設定，無需附上任何理由，也不會因此衍生出任何不利，或影響您既有的權利。</li>
                </ul>
                <p>若您在表單填寫過程中，對於問題本身敘述、用字或選項有任何疑慮，敬請參考表單中說明或進一步詢問研究人員。</p>
             -->
                <p>您是否有受邀參加與人體相關的學術研究案？</p>
                <p>您是否好奇過這些研究案都做了些什麼？</p>
                <p>您是否有想過自己的資料可以創造更大的價值？</p>
                <!-- <p>個資權利管理系統（Rights Management System，簡稱RMS），是以使用者（即個資當事人）意願為核心，在當事人同意匯入的<label style="text-decoration: underline;" class="tooltip-top space-x-2 tooltip sm:tooltip-top whitespace-pre-line" :data-tip="'RMS僅是資料權利與資訊的匯流，並不持有資料本身。'">資料權利<img style="float: right;" src="../../assets/image/datausage01.png" alt=""></label>範圍內，將資料的<label style="text-decoration: underline;" class="tooltip-top space-x-2 tooltip sm:tooltip-top whitespace-pre-line" :data-tip="'RMS僅是資料權利與資訊的匯流，並不持有資料本身。'">相關資訊<img style="float: right;" src="../../assets/image/datausage01.png" alt=""></label>進行整合，使用者還可以透過右上角偏好設定功能進行管理。</p> -->
                <p>個資權利管理系統（Rights Management System，簡稱RMS），是以使用者（即個資當事人）意願為核心，在當事人同意匯入的<label style="text-decoration: underline;color: #004B97;" class="tooltip-top space-x-2 tooltip sm:tooltip-top whitespace-pre-line" :data-tip="'RMS僅是資料權利與資訊的匯流，並不持有資料本身。'">資料權利</label>範圍內，將資料的<label style="text-decoration: underline;color: #004B97;" class="tooltip-top space-x-2 tooltip sm:tooltip-top whitespace-pre-line" :data-tip="'例如資訊從何而來、由誰申請、如何利用等資訊'">相關資訊</label>進行整合，使用者還可以透過右上角偏好設定功能進行管理。</p>
                <p>而透過資料再利用清單，使用者能即時掌握該範圍資料有誰想使用、誰在正使用等動態資訊。</p>
                
                <div class="alert alert-error shadow-lg flex justify-between text-white">
                    <div>
                        <svg class=" fill-white" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24"><path fill="none" d="M0 0h24v24H0z"/><path d="M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-2a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm-1-5h2v2h-2v-2zm0-8h2v6h-2V7z"/></svg>
                        <div class=" space-x-4 inline-flex">
                            <p> 您是否同意使用RMS管理您的資料權利？ </p>
                        </div>
                    </div>
                    <div>
                        <label class="flex" for="agreenYes">
                            <input type="radio" id="agreenYes" name="agreeRadio" value="true" v-model="agreeRadio"  class="radio mr-2" />是
                        </label>
                        <label class="flex" for="agreenNo">
                            <input type="radio" id="agreenNo" name="agreeRadio" value="false" v-model="agreeRadio" class="radio mr-2" />否
                        </label>
                    </div>
                </div>
            </div>
            <div class="text-center">
                <!-- <router-link class="btn w-40" :class="{'btn-disabled': agreeRadio == 'false' || agreeRadio == ''}" :to="{path: '/dataType', query: { setup: 'type', question: 1  }}">確定</router-link> -->
                <template v-if="agreeRadio == 'true'">
                    <button class="btn w-40" @click="setAgree(1)">確定</button>
                </template>
                <template v-else-if="agreeRadio == 'false'">
                    <button class="btn w-40" @click="setAgree(0)">返回</button>
                </template>
            
            </div>
        </div>
    </div>
</template>
<script>
    import { defineComponent, ref, } from 'vue';
    import { useRouter } from 'vue-router'
    import { useStore } from "vuex";
export default defineComponent({
    setup() {
        const store = useStore();
        const router = useRouter()
        const agreeRadio = ref("");    

        const setAgree = (ans) =>{

            if(ans){
                sessionStorage.setItem("isAgree", ans);
                router.push({name:"Home"})
            }
            else
            {
                store.commit("auth/removeUser");
                router.push({name:"Login"})
            }
        }
        
        return {
            router,
            store,
            agreeRadio,
            setAgree
        }
    }
})
</script>
<style lang="postcss" scoped>
    .agree {
        @apply flex justify-center min-h-screen text-justify p-6 sm:p-0;
    }
</style>