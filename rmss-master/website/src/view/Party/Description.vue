<template>
    <div class="agree">
        <div class="max-w-xl space-y-8">
            <h1 class="step-title">
                說明
            </h1>
            <div class="space-y-4 bg-white p-8 border rounded-xl ">
                <p>個資權利管理系統（<label style="font-weight: bolder;">Rights Management System</label>，簡稱<label style="font-weight: bolder;">RMS</label>），是以使用者（即個資當事人）意願為核心，在當事人同意匯入的<label style="font-weight: bolder;text-decoration: underline;">資料權利</label>範圍內，將資料的相關資訊進行整合，使用者可以使用偏好設定功能，進行與匯入資料相關的權利管理。</p>
                <p>例如，我的健檢資料，想提供給國內的公共學術研究作使用；例如，我的精神疾病診斷資料想提供給跨域的商業研究作使用，都能透過偏好功能進行設定</p>
                <p><label style="color:red">請注意</label>，<label style="font-weight: bolder;">RMS</label>是資料資訊與權利的匯流，也就是說<label style="font-weight: bolder;">RMS</label><label style="font-weight: bolder;text-decoration: underline;">只會知道有這份資料的存在</label>，並協助蒐集該份資料的動態資訊，並<label style="font-weight: bolder;text-decoration: underline;">不持有該資料本身</label>，若<label style="font-weight: bolder;text-decoration: underline;">對資料本身有任何問題，請向資料持有機關聯繫。</label></p>

            </div>
            <div class="text-center">
                <button class="btn w-40" @click="router.go(-1)">返回</button>
            </div>
        </div>
    </div>
</template>
<script>
    import { defineComponent, ref, } from 'vue';
    import { useRouter } from 'vue-router'
export default defineComponent({
    setup() {
        const router = useRouter()
        
        
        return {
            router
        }
    }
})
</script>
<style lang="postcss" scoped>
    .agree {
        @apply flex justify-center min-h-screen text-justify p-6 sm:p-0;
    }
</style>