
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
        <template v-if="($route.query.setup === 'descr' || $route.query.setup === 'type' || $route.query.setup === 'range')">
            <ul class="steps grid my-8">
                <!-- <li data-content="" class="step step-primary">資料類型設定</li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' || $route.query.question != 1}"></li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' || $route.query.question == 3}"></li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range'}">資料再利用設定</li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' && $route.query.question != 1}"></li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range' && $route.query.question == 3}"></li>
                <li data-content="" class="step">偏好設定一覽表</li> -->
                <li data-content="" class="step step-primary">RMS的說明</li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'type' || $route.query.setup === 'range' }">資料類型設定</li>
                <li data-content="" class="step" :class="{ 'step-primary': $route.query.setup === 'range'}">資料再利用設定</li>
            </ul>
            <div class="divider"></div> 
        </template>

         <div>
            <template v-if="$route.query.setup == 'descr'">
                <h3 class="text-3xl mb-6 text-primary-700 text-center">RMS的說明</h3>
                <Transition name="fade">
                    <div>
                        <p style="margin: 1rem;">RMS採用的二階偏好選項表單，同時採用了過往常見的「同意」或「不同意」兩種選擇，並提供了詳細資料類型與資料再利用設定，供使用者點選。</p>
                        <p style="margin: 1rem;">接下來的資料類型與資料再利用設定，會透過模擬情境，讓您更好地理解偏好設定的意義並引領您進行設定。</p>
                    </div>
                </Transition>
                <!-- <div class="alert alert-error shadow-lg flex justify-between text-white mt-2">
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
                </div> -->
            </template>
            <!-- 右上方細項操作 -->
            <template v-else-if="$route.query.setup == 'type'">
                <h3 class="text-3xl mb-6 text-primary-700 text-center">資料類型設定
                    <label class="relative inline-flex items-center cursor-pointer float-right">
                        <input type="checkbox" v-model="isDetail.main1" class="sr-only peer">
                        <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
                        <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300">細項設定</span>
                    </label>
                </h3>
            </template>
            <template v-else-if="$route.query.setup == 'range'">
                <h3  class="text-3xl mb-6 text-primary-700 text-center">資料再利用設定
                    <label class="relative inline-flex items-center cursor-pointer float-right">
                        <input type="checkbox" v-model="isDetail.main2" class="sr-only peer">
                        <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
                        <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300">細項設定</span>
                    </label>
                </h3>
                
            </template>
            
            <!-- 切換詳細資料 -->
            <template v-if="(!isDetail.main1 && $route.query.setup == 'type') || (!isDetail.main2 && $route.query.setup == 'range')">
                <div class="flex flex-col lg:flex-row sm:space-x-12 items-center">
                    <div class="w-full lg:w-1/3 ">
                        <template v-if="$route.query.setup == 'type'">
                            <div class="grid grid-cols-1 pt-6 pr-6 pb-3 rounded-lg text-center place-items-center">
                                <div class="col-span-1">
                                    <img src="../../assets/image/path01.png" alt="" class="inline sm:w-8/12">
                                </div>
                                <div class="col-span-1 text-xl">
                                    <!-- <div > 圖片下方說明文字 </div> -->
                                </div>
                            </div>
                        </template>
                        <template v-if="$route.query.setup == 'range'">
                            <div class="grid grid-cols-1 pt-6 pr-6 pb-3 rounded-lg text-center place-items-center">
                                <div class="col-span-1">
                                    <img src="../../assets/image/path04.png" alt="" class="inline w-8/12">
                                </div>
                                <div class="col-span-1 text-xl">
                                    <!-- <div > 圖片下方說明文字 </div> -->
                                </div>
                            </div>
                        </template>
                    </div>
                    
                    <div class="w-full lg:w-2/3">
                        <template v-if="$route.query.setup == 'type'">
                            <div class="text-xl my-12">
                                您的資料，可能因為進行診斷或治療、為預防疾病而進行檢查或是因為參與學術研究，而留存在不同機構，這些資料，您是否同意開放利用？
                            </div>
                            <div class="grid sm:grid-rows-2 grid-cols-5 my-5 border pt-6 pr-6 pb-3 bg-white rounded-lg">

                                <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                    <input type="radio" :name="'radio-DM01'" class="radio" :value="1" v-model="showContSubMain" @change="changeMainSelect" />
                                    <span class="inline-block align-top ml-1">同意</span>
                                </div>
                                <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                    <input type="radio" :name="'radio-DM01'" class="radio" :value="4" v-model="showContSubMain" @change="changeMainSelect" />
                                    <span class="inline-block align-top ml-1">不同意</span>
                                </div>
                                <div class="sm:col-span-4 col-span-4 radioBlock px-3 py-1 text-rose-500">
                                    如需更詳細之偏好設定，可點選右上方開關切換到細項設定！
                                </div>
                                <!-- <div class="sm:col-span-4 col-span-4 radioBlock px-3 py-1 text-rose-500">
                                   <label class="relative inline-flex items-center cursor-pointer">
                                        <input type="checkbox" v-model="isDetail.main1" class="sr-only peer">
                                        <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
                                        <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300">細項設定</span>
                                    </label>
                                </div> -->
                            </div>
                        </template>
                        <template v-else-if="$route.query.setup == 'range'">
                            <div class="text-xl my-12">
                                可能會有不同經費來源、基於學術研究或是商業目的、或是涉及跨境合作的研究計畫，申請利用您的資料，您是否同意開放他們利用？
                            </div>
                            <div class="grid sm:grid-rows-2 grid-cols-5 my-5 border pt-6 pr-6 pb-3 bg-white rounded-lg">

                                <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                    <input type="radio" :name="'radio-DM01'" class="radio" :value="1" v-model="showContSubMain" @change="changeMainSelect" />
                                    <span class="inline-block align-top ml-1">同意</span>
                                </div>
                                <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                    <input type="radio" :name="'radio-DM01'" class="radio" :value="4" v-model="showContSubMain" @change="changeMainSelect" />
                                    <span class="inline-block align-top ml-1">不同意</span>
                                </div>
                                <div class="sm:col-span-4 col-span-4 radioBlock px-3 py-1 text-rose-500">
                                    如需更詳細之偏好設定，可點選右上方開關切換到細項設定！
                                </div>
                                 <!-- <div class="sm:col-span-4 col-span-4 radioBlock px-3 py-1 text-rose-500">
                                    <label class="relative inline-flex items-center cursor-pointer">
                                        <input type="checkbox" v-model="isDetail.main2" class="sr-only peer">
                                        <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
                                        <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300">細項設定</span>
                                    </label>
                                </div> -->

                                
                            </div>
                        </template>
                    </div>
                </div>
                
            </template> 
            <template v-else-if="(isDetail.main1 && $route.query.setup == 'type') || (isDetail.main2 && $route.query.setup == 'range')">
                <template v-for="(iteml3, index3) in getContByArray(getTitle(0))" :key="index3">
                    <div class="flex flex-col lg:flex-row sm:space-x-12 items-center">
                        <!-- 圖片說明 -->
                        <div class="w-full lg:w-1/3 ">
                            <template v-if="$route.query.setup == 'type'">
                                <div class="grid grid-cols-1 pt-6 pr-6 pb-3 rounded-lg text-center place-items-center">
                                     <div class="col-span-1 text-xl">
                                        <div v-if="index3 == 0" style="margin-bottom: 0.5rem;font-size: 2rem;">診斷治療資料</div>
                                        <div v-if="index3 == 1" style="margin-bottom: 0.5rem;font-size: 2rem;">預防健檢資料</div>
                                        <div v-if="index3 == 2" style="margin-bottom: 0.5rem;font-size: 2rem;">參與學術研究資料</div>
                                    </div>
                                    <div class="col-span-1">
                                        <img v-if="index3 == 0" src="../../assets/image/path01.png" alt="" class="inline sm:w-8/12">
                                        <img v-if="index3 == 1" src="../../assets/image/path02.png" alt="" class="inline sm:w-8/12">
                                        <img v-if="index3 == 2" src="../../assets/image/path03.png" alt="" class="inline sm:w-8/12">
                                    </div>
                                    <div class="col-span-1 text-xl">
                                        <div v-if="index3 == 0">我們在看醫生的時候，醫生會進行問診、診斷與治療，因此所產生的資料</div>
                                        <div v-if="index3 == 1">我可能為了就學、就業而去做的預防健康檢查，或者是例行性的自費健檢，所產生的資料</div>
                                        <div v-if="index3 == 2">我參加了一些疾病或是疫苗等醫療相關的研究計畫，額外做了其他檢查或病理檢驗所產生的資料</div>
                                    </div>
                                </div>
                            </template>
                            <template v-if="$route.query.setup == 'range'">
                                <div class="grid grid-cols-1 pt-6 pr-6 pb-3 rounded-lg text-center place-items-center">
                                   <div class="col-span-1 text-xl">
                                        <div v-if="index3 == 0" style="margin-bottom: 0.5rem;font-size: 2rem;">經費來源</div>
                                        <div v-if="index3 == 1" style="margin-bottom: 0.5rem;font-size: 2rem;">預期研究目的</div>
                                        <div v-if="index3 == 2" style="margin-bottom: 0.5rem;font-size: 2rem;">國內或跨境利用</div>
                                    </div>
                                    <div class="col-span-1">
                                        <img v-if="index3 == 0" src="../../assets/image/path04.png" alt="" class="inline w-8/12">
                                        <img v-if="index3 == 1" src="../../assets/image/path05.png" alt="" class="inline w-8/12">
                                        <img v-if="index3 == 2" src="../../assets/image/path06.png" alt="" class="inline w-8/12">
                                    </div>
                                    <div class="col-span-1 text-xl">
                                        <div v-if="index3 == 0">研究計畫的經費來源可能是政府、可能是非政府單位，我需要想一想「誰出錢」這件事，會不會影響我提供資料的意願？</div>
                                        <div v-if="index3 == 1">我的資料被研究計畫拿去用，是基於學術目的？還是有利益商業目的？</div>
                                        <div v-if="index3 == 2">研究計畫可能是本地的，也可能有境外機構或他國勢力之參與，並對資料進行蒐集、儲存、分析，我是否願意提供資料給他們再利用？</div>
                                    </div>
                                </div>
                            </template>
                        </div>
                        
                        <div class="w-full lg:w-2/3 ">
                            <div class="text-xl my-12">
                                <template v-if="$route.query.setup == 'type'">
                                    資料區分單純文字與<label style="text-decoration: underline;color: #004B97;" class="tooltip-top space-x-2 tooltip sm:tooltip-top whitespace-pre-line" :data-tip="'例如X光、CT、MRI、心電圖等影像資料。'">影像</label>的紀錄資料與有生物資訊的檢體，請依您的意願勾選下方偏好設定:
                                </template>
                                <template v-else-if="$route.query.setup == 'range'">
                                    <span v-if="index3 == 0">經費來源區分本國公部門與非本國公部門之補助，請依您的意願勾選下方偏好設定：</span>
                                    <span v-if="index3 == 1">研究的預期目的區分商業目的為主與學術目的，請依您的意願勾選下方偏好設定：</span>
                                    <span v-if="index3 == 2">研究計畫區分國內或跨境利用，請依您的意願勾選下方偏好設定：</span>
                                </template>
                            </div>

                            <!-- 欄位 -->
                            <template v-for="(iteml2, index2) in iteml3" :key="index2">
                                <div class="grid sm:grid-rows-2 grid-cols-5 my-5 border pt-6 pr-6 pb-3 bg-white rounded-lg">
                                    <div v-if="$route.query.setup == 'type'" class="col-span-5 px-3 text-xl">
                                        Q{{ index2+1 }}: 
                                        <span v-if="index2 == 0">紀錄資料</span>
                                        <span v-else>檢體</span>
                                    </div>
                                    <div v-else-if="$route.query.setup == 'range'" class="col-span-5 px-3 text-xl">
                                        Q{{ index2+1 }}: 
                                        <template v-if="index3 == 0">
                                            <span v-if="index2 == 0">公部門（例如中央或地方政府、公立大學／醫院）</span>
                                            <span v-else>非公部門（例如由民間企業、私立大學／醫院、或NGO等資助經費占50%以上）</span>
                                        </template>
                                        <template v-if="index3 == 1">
                                            <span v-if="index2 == 0">商業目的（例如技術轉移收取權利金、研發或改良新型藥物或器材以銷售為目的）</span>
                                            <span v-else>學術目的（純粹學術研究、公部門之政策依據或立法研究）</span>
                                        </template>
                                        <template v-if="index3 == 2">
                                            <span v-if="index2 == 0">研究計畫涉及跨境合作，舉凡經費來源、計畫規劃或執行過程有無境外機構或他國勢力的參與，甚至資料是由非中華民國台灣境內的人所蒐集、分析，均包括在內。</span>
                                            <span v-else>研究計畫未涉及任何跨國合作</span>
                                        </template>
                                    </div>
                                    <!-- <div v-if="$route.query.setup == 'range'" class="sm:row-span-2 col-span-1 row-span-5 flex justify-center items-center stepNumber">{{ iteml2.DC_NUM }}</div>
                                    <div v-if="$route.query.setup == 'range'" class="col-span-4"><div class="mb-4" v-html="iteml2.CCONTENT"></div></div> -->

                                    <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                        <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" :value="1" v-model="iteml2.ANS" @click="funAllNot(true, iteml2)" @change="checkMainSelected" />
                                        <span class="inline-block align-top ml-1">完全開放</span>
                                    </div>
                                    <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                        <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" :value="4" v-model="iteml2.ANS" @click="funAllNot(false, iteml2)"  @change="checkMainSelected" />
                                        <span class="inline-block align-top ml-1">完全不開放</span>
                                    </div>
                                    <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                        <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" :value="3" v-model="iteml2.ANS" @change="checkMainSelected"/>
                                        <span class="inline-block align-top ml-1">逐次決定</span>
                                    </div>
                                    <div class="sm:col-span-1 col-span-4 radioBlock px-3 py-1">
                                        <input type="radio" :name="'radio-' + iteml2.DC_NUM" class="radio" :value="2" @click="handleShowCheck(iteml2.DC_NUM, iteml2)" v-model="iteml2.ANS" @change="checkMainSelected" />
                                        <span class="inline-block align-top ml-1">選擇性開放</span>
                                    </div>
                                </div>
                            </template>
                            
                        </div>

                        
                    </div>
                </template>

                
                
            </template>

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
            <button v-if="$route.query.setup == 'type' || $route.query.setup == 'range' " class="btn w-40 mb-3" @click="goBack()">上一步</button>
            <button v-if="$route.query.setup == 'descr'|| $route.query.setup == 'type'" class="btn w-40 mb-3" @click="goNext()">下一步</button>
            <button v-if="$route.query.setup == 'range'" class="btn w-40" @click="TempContSub()">確定</button>
<!-- 
            <button v-if="($route.query.setup != 'unit') && !($route.query.setup == 'type' && $route.query.question == 1)" class="btn w-40 mb-3" @click="goType()">上一步</button>
            <button v-if="($route.query.setup != 'unit') && !($route.query.setup == 'range' && $route.query.question == 3)" class="btn w-40" @click="goRange()">下一步</button>
            <button v-if="$route.query.setup == 'range' && $route.query.question == 3" class="btn w-40" @click="TempContSub()">下一步</button>
           -->
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
        const showContSubMain = ref(0);
        const dataConSubOne = reactive([]);
        const subChkAll = ref(false);
        const subChkNo = ref(false);
        const openTip = ref({ full: "代表您同意完全開放您的資料被再利用。", step: "代表平台應逐次詢問您的開放意願。", select: "代表在符合特殊目的或情境下，您同意開放特定性質的資料被再利用。", non: "代表您不同意您的資料被再利用。" });
        const selectTitle = ref(["", "您因診斷或治療疾病所需而生的紀錄資料中", "您因診斷或治療疾病所需而被採集的檢體中", "您因參加預防健檢而衍生出的紀錄資料中", "您因參加預防健檢而被採集的檢體中", "您因參與學術研究而衍生出的紀錄資料中", "您因參與學術研究而被採集的檢體中", "受本國公部門補助的研究計畫應符合以下研究方向", "非受本國公部門補助的研究計畫應符合以下研究方向", "預期有商業目的的研究計畫應符合以下研究方向", "預期僅用於學術目的的研究計畫應符合以下研究方向", "未涉及跨境合作的研究計畫應符合以下研究方向", "涉及跨境合作的研究計畫應符合以下研究方向"
        ]);
        const isDetail = reactive({
            main1:false,
            main2:false,
        });
        const agreeRadio = ref("");  
        const reqData2 = reactive({
            TokenID: "",
            Token: "",
            Page: "",
            ContAnsList: []
        });

        //------功能
        // 初始
        const initData = () => {
            if (route.query.setup == "descr") { //說明頁面

            }
            else if (route.query.setup == "range") { //資料利用
                showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM02");
                showContSubMain.value  = 0
                checkMainSelected()
            }
            else if (route.query.setup == "type") { //資料類型
                showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM01");
                showContSubMain.value  = 0
                checkMainSelected()
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
        
            console.log(list,89789798)
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
                        // console.log(Intersection)
                        if(Intersection.length > 0){
                            count+=1
                        }
                    }
                    
                    // console.log(count)
                    // console.log(ConSub)

                    
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

                if(route.query.setup == "unit")
                {
                    router.push({name: 'DataUsage',query:{from:"unit"}});
                }
                else{
                    // router.push({ path: "/" });
                    handleSave()
                }
            }
            else
                alert("尚有資料未填! 請確認填寫後至下一頁");
        };
        // 取得標題列表
        const getTitle = (value) => {
            if (showContSub.value != null) {
                // const titleList = showContSub.value.filter(item => item.DC_MAIN == pageType.value);
                const titleList = showContSub.value.map(item => item.CTITLE).filter(onlyUnique);
                //回傳全部
                if(value == 0)
                    return titleList;
                else
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
        // 依標題陣列，取得題目內容
        const getContByArray = (ctitleArray) => {
            if (showContSub.value != null) {
                const contList = showContSub.value.filter(item => ctitleArray.includes(item.CTITLE));
                contList.forEach(item => { item.ACOTHER = item.ACOTHER == "" ? "50" : item.ACOTHER; });
                contList.forEach(item => { item.CCONTENT = item.CCONTENT.replace("右側", "以下") });
                
                var contListTemp = []
                //重組每兩項為一組
                for(var i = 0;i < contList.length;i++){
                    var list = []
                    list.push(contList[i])
                    list.push(contList[i+1])
                    contListTemp.push(list)
                    i++
                }
                return contListTemp;
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

        // 按鈕 至資料使用設定頁  --舊版寫法
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
        // 按鈕 至資料類型設定頁 --舊版寫法
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
        // 按鈕 至下頁
        const goNext = () =>{
            if (route.query.setup == "descr"){
                router.push({ path: "/dataType", query: { setup: "type"} }).then(()=>{
                    showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM01");
                    showContSubMain.value  = 0
                    checkMainSelected()
                });
                
            }
            else if (route.query.setup == "type") {
                router.push({ path: "/dataType", query: { setup: "range"} }).then(() => {
                    showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM02");
                    showContSubMain.value  = 0
                    checkMainSelected()
                });
            }
        };
        // 按鈕 至上頁
        const goBack = () =>{
            if (route.query.setup == "type"){
                router.push({ path: "/dataType", query: { setup: "descr"} });
            }
            else if (route.query.setup == "range") {
                router.push({ path: "/dataType", query: { setup: "type"} }).then(()=>{
                    showContSub.value = dataContSub.value.filter(item => item.DC_MAIN == "DM01");
                    showContSubMain.value  = 0
                    checkMainSelected()
                });
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

        //從現在show的項目 判斷總體同意或不同意
        const checkMainSelected = () =>{
            let count = 0
            for(var i in showContSub.value){
                if(showContSub.value[i].ANS == 0){
                    count = 0
                    break
                }
                count +=  showContSub.value[i].ANS  
            }

            if(count == 6)
                showContSubMain.value = 1
            else if(count == 24)
                showContSubMain.value = 4
            else if(count != 0){
                showContSubMain.value = 0
                if(route.query.setup == 'type')
                  isDetail.main1 = true
                else if(route.query.setup == 'range')
                  isDetail.main2 = true
            }
            
        }
        const changeMainSelect = () =>{
            
            if(showContSubMain.value == 1 || showContSubMain.value == 4){
                for(var i in showContSub.value){
                    showContSub.value[i].ANS = showContSubMain.value 
                }
            }
        }

        // 按鈕 儲存
        const handleSave = () => {
            reqData2.ContAnsList = dataContSub.value;

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
            reqData2.ContAnsOtherList = arrayTemp
            // console.log(reqData)
            apiHander.FunctionToken(checkPrivated, reqData);
            //apiHander.FunctionToken(addContAns, reqData);
        }

        // ---------- api
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
                        router.push({
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
                        router.push({
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
                        //console.log(isPried);
                        if (isPried) {
                            // classRouter.push({ path: "/" });
                            apiHander.FunctionToken(updateContAns, reqData2);
                        }
                        else {
                            //dataContSub.value = JSON.parse(sessionStorage.getItem("ConSub"));
                            apiHander.FunctionToken(addContAns, reqData2);
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
            isDetail,
            agreeRadio,
            isLoading,
            dataContSub,
            getTitle,
            getTitleAll,
            getCont,
            getContByArray,
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
            goNext,
            goBack,
            showContSub,
            showContSubMain,
            openTip,
            funAllNot2,
            checked,
            unitModel,
            unitDataSub,
            checkMainSelected,
            changeMainSelect,
            handleSave,
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