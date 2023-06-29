<template>
  <div class="navbar bg-base-100 shadow-md mb-10">
    <div class="flex-1">
        <!-- <router-link to="/"> -->
        <router-link :to="test() ? '/v2' : '/'">
            <img src="../assets/image/logo_horizon.svg" class=" hidden sm:block w-2/3" alt="">
            <img src="../assets/image/logo_horizon_sm.svg" class=" sm:hidden w-2/3" alt="">
        </router-link>
    </div>
    <div class="flex-none">
      <div class=" space-x-2 mr-2" @mouseleave="isReadCount('mouseleave')">
        <div class="dropdown dropdown-end" :class="{'dropdown-hover': states.uaertype == 0}" >
          <label tabindex="0" class="btn btn-sm btn-ghost indicator" @click="APMIsRead">
            <span class="indicator-item indicator-start badge badge-secondary">{{ isReadCount('number') }}</span>
            <template v-if="states.uaertype == 0">
              <svg class="inline-block mr-1 fill-primary-500" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24"><path fill="none" d="M0 0h24v24H0z"/><path d="M14 14.252V22H4a8 8 0 0 1 10-7.748zM12 13c-3.315 0-6-2.685-6-6s2.685-6 6-6 6 2.685 6 6-2.685 6-6 6zm6 4v-3.5l5 4.5-5 4.5V19h-3v-2h3z"/></svg>
            </template>
            <template v-else-if="states.uaertype == 1">
              <svg class="inline-block mr-1 fill-secondary-700" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24"><path fill="none" d="M0 0h24v24H0z"/><path d="M14 14.252V22H4a8 8 0 0 1 10-7.748zM12 13c-3.315 0-6-2.685-6-6s2.685-6 6-6 6 2.685 6 6-2.685 6-6 6zm8 4h3v2h-3v3.5L15 18l5-4.5V17z"/></svg>
            </template>
            {{states.username}}
          </label>
          <div v-if="states.uaertype == 0" tabindex="0" class="dropdown-content card card-compact p-2 shadow bg-primary text-primary-content bg-white lg:w-96 sm:w-56">
            <div class="card-body overflow-auto" :style="isReadCount('style') > 10 ? 'height: 70vh' : ''">
              <template v-for="(item, index) in NotifyList" :key="index">
                <div class="grid grid-cols-4 rounded-md hover:bg-primary-200">
                  <div class="col-span-3 p-1">
                    <div :class="item.isRead ? '' : 'font-bold'">{{ item.PlanName }} 資料申請</div>
                    <div>{{dateConvert(item.applyDate)}}</div>
                  </div>
                  <label for="notifies-modal" class="col-span-1 btn btn-xs sm:btn-md" @click="notifiesModalInfo(item)">詳情</label>
                </div>
              </template>
            </div>
          </div>  
          <div v-if="states.uaertype == 1 && isReadCount('number')" tabindex="0" class="dropdown-content card card-compact p-2 shadow bg-primary text-primary-content bg-white lg:w-96 sm:w-56">
            <div class="card-body overflow-auto" :style="isReadCount('style') > 10 ? 'height: 70vh' : ''">
              <template v-for="(item, index) in NotifyList" :key="index">
                <template v-for="(item2, index2) in item.MatchList" :key="index2">
                  <div v-if="!item2.ApmIsRead" class="grid grid-cols-4 rounded-md hover:bg-primary-200">
                    <div class="col-span-4 p-1">
                      <strong>{{ item.PlanName }}</strong>
                      申請資料編號{{ item2.number }}經個資當事人
                      <span class="text-success" v-if="item2.state == 2">同意</span>
                      <span class="text-error" v-if="item2.state == 3">拒絕</span>
                    </div>
                    <!-- <router-link :to="{path: '/PlanMatch', query: { apmId: item2.ApmID }}" class="btn btn-xs sm:btn-md">詳情</router-link> -->
                  </div>
                </template>
              </template>
            </div>
          </div>
        </div>
      </div>

      <div class="dropdown dropdown-end">
        <label tabindex="0" class="btn btn-square btn-ghost">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                class="inline-block w-5 h-5 stroke-current">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M4 6h16M4 12h16M4 18h16"></path>
            </svg>
        </label>
        <ul tabindex="0" class="dropdown-content menu p-2 shadow bg-base-100 rounded-box w-52">
            <li>
              <!-- <router-link to="/"> -->
                <!-- <template v-if="window.location.href.includes('v2')">
                  dddd
                </template> -->
              <router-link :to="test() ? '/v2' : '/'">
                <span v-if="states.uaertype == 0">資料再利用清單</span>
                <span v-else-if="states.uaertype == 1">資料再利用清單</span>
              </router-link>
            </li>
            <template v-if="states.uaertype == 0">
              <li>
                <!-- <router-link :to="{path: '/dataType', query: { setup: 'type', question: 1 }}">偏好設定</router-link> -->
                <!-- <router-link :to="{path: test() ? '/dataType/v2' : '/dataType', query: { setup: 'type', question: 1 }}">偏好設定</router-link> -->
                <router-link :to="{path: test() ? '/dataType/v2' : '/dataType', query: { setup: 'descr' }}">偏好設定</router-link>
              </li>
              <li>
                <!-- <router-link :to="{path: '/agree'}">偏好設定說明</router-link> -->
                <router-link :to="{path: test() ? '/agree/v2' : '/dataUsage'}">偏好設定一覽表</router-link>
              </li>
              <!-- <li>
                  <router-link :to="{path: '/dataType', query: { setup: 'range' }}">資料再利用的類型設定</router-link>
              </li>
              <li>
                  <router-link to="/dataUsage">資料再利用偏好設定</router-link>
              </li> -->
              <li>
                <router-link :to="{path: '/description'}">說明</router-link>
              </li>
              <li>
                <router-link :to="{path: '/inheritance'}">數位遺產</router-link>
              </li>
            </template> 
            <li>
              <a @click="handleLogout">登出</a>
            </li>
        </ul>
      </div>
    </div>
    <input type="checkbox" id="notifies-modal" class="modal-toggle"/>
    <div class="modal">
      <div class="modal-box">
        <div class="flex justify-end"><label for="notifies-modal" class="btn w-1/6" @click="notifiesModalClose(usageListInfo)">X</label></div>
        <div class="font-bold text-lg">{{ usageListInfo.PlanName }}</div>
        <div class="divider"></div>
        <p>計畫編號: {{ usageListInfo.apmNo }}</p>
        <p>計畫類型: {{ usageListInfo.planType }}</p>
        <p>申請資料來源: {{ usageListInfo.applySource }}</p>
        <p>申請資料類型: {{ usageListInfo.applyType }}</p>
        <p>申請日期:  {{ dateConvert(usageListInfo.applyDate) }}</p>
        <p>偏好設定: {{ usageListInfo.preference }}</p>

        <p>研究機構/執行單位(院校/科部系所): {{ usageListInfo.ResearchinsTitute }}</p>
        <p>委託單位/藥廠: {{ usageListInfo.Requester }}</p>
        <p>經費來源: {{ usageListInfo.Funding }}</p>
        <p>主要主持人: {{ usageListInfo.HostName }}</p>
        <p>協同主持人: {{ usageListInfo.ViceHostName }}</p>
        <p>聯絡人: {{ usageListInfo.ContactPerson }}</p>
        <p>上班時間聯絡電話: {{ usageListInfo.ContactNumber }}</p>
        <p>研究目的: {{ usageListInfo.Purpose }}</p>
      </div>
    </div>
  </div>
</template>
<script>
  import {defineComponent, reactive, computed, ref, onBeforeMount} from 'vue';
  import { useStore } from "vuex";
  import { apihandler } from '../api/apiHandler.js';
  import { useRouter } from 'vue-router';
  
  export default defineComponent({
    setup (){
      const store = useStore();
      const apiHander = apihandler();
      const router = useRouter()
      const handleLogout = () => {
          store.commit("auth/removeUser");
          sessionStorage.removeItem("Version");
          router.push({path: '/Login'});
          // location.reload();
      }
      const states = reactive({
          username: computed(()=> store.state.auth.user.username),
          uaertype: computed(()=> store.state.auth.user.userType),
      });
      const NotifyList = ref({});
      const usageListInfo = ref({
        PlanName: "",
        EnglishName: "",
        ResearchinsTitute: "",
        Requester: "",
        Funding: "",
        HostName: "",
        HostJobTtitle: "",
        ViceHostName: "",
        ViceHostJobTtitle: "",
        ContactPerson: "",
        ContactNumber: "",
        Purpose: ""
      });

      // 版本二 測試用
      const test = () => {
        // console.log(sessionStorage.getItem("Version"))
        // const value = window.location.href.includes("v2")
        // return window.location.href.includes("v2")
        return sessionStorage.getItem("Version") ? true : false
      }

      // "number"未讀筆數、"style"
      const isReadCount = (check) => {
        let value = 0;
        if (NotifyList.value) Object.values(NotifyList.value).map(x => {if(x.MatchList) value += x.MatchList.length})

        switch(check) {
          case "number": {
            if (states.uaertype) {
              return value;
            }else 
              return NotifyList.value ? Object.values(NotifyList.value).filter(item => item.isRead == false).length : 0;
          }
          case "style": {
            if (states.uaertype) {
              return value;
            }else
              return NotifyList.value.length
          }
          case "mouseleave": 
            if(states.uaertype == 1 && value) apiHander.FunctionToken(getBackNotifyList, reqBasic.value);
        }
      }
      // notifies-modal info
      const notifiesModalInfo = (info) => {
        // info.isRead = true;
        usageListInfo.value = info;
      }
      // 變更已讀
      const notifiesModalClose = (item) => {
        if (!item.isRead) {
          item.isRead = true;
          reqBasic.value.AcID = item.AcID;
          apiHander.FunctionToken(notifyListIsRead, reqBasic.value);
        }
      } 
      const APMIsRead = () => {
        if (states.uaertype == 1) {
          let value = 0;
          Object.values(NotifyList.value).map(item => value += item.MatchList.length)
          if (value) {
            let result = []
            Object.values(NotifyList.value).map(item => item.MatchList.map(x => result.push({"apmId": x.apmId  ,"acId": x.acId})))
            reqBasic.value.AcID = JSON.stringify(result);
            apiHander.FunctionToken(notifyListAPMIsRead, reqBasic.value);
          }
        }
      }

      // 日期轉換
      const dateConvert = (date) => {
          const mydate = new Date(date);
          const y = mydate.getFullYear();
          const m = (mydate.getMonth() + 1).toString().length == 1 ? "0" + (mydate.getMonth() + 1) : mydate.getMonth() + 1;
          const d = mydate.getDate().toString().length == 1 ? "0" + mydate.getDate() : mydate.getDate();
          return `${y}-${m}-${d}`;
      };

      ///===== api
      const reqBasic = ref({
        TokenID: "",
        Token: "",
        Page: ""
      });
      const apiGetReviewNotifyList = (data) => apiHander.userRequest.post("api/Notify/GetReviewNotifyList", data);
      const getReviewNotifyList = (data) => {
        apiGetReviewNotifyList(data)
          .then((res) => {
            const json = res.data;
            if (json.Status) {
              NotifyList.value = JSON.parse(json.Data);
              // console.log(NotifyList.value)
            }
          })
          .catch((err) => {
            console.log(err);
          });
      }

      const apiGetBackNotifyList = (data) => apiHander.userRequest.post("api/Notify/GetBackNotifyList", data);
      const getBackNotifyList = (data) => {
        apiGetBackNotifyList(data)
          .then((res) => {
            const json = res.data;
            if (json.Status) {
              NotifyList.value = JSON.parse(json.Data);
              // console.log(NotifyList.value)
            }
          })
          .catch((err) => {
            console.log(err);
          });
      }

      const apiNotifyListIsRead = (data) => apiHander.userRequest.post("api/Notify/NotifyListIsRead", data);
      const notifyListIsRead = (data) => {
        apiNotifyListIsRead(data)
          .then((res) => {
            const json = res.data;
            if (json.Status) {
              apiHander.FunctionToken(getReviewNotifyList, reqBasic.value);
            }
          })
          .catch((err) => {
            console.log(err);
          });
      }

      const apiNotifyListAPMIsRead = (data) => apiHander.userRequest.post("api/Notify/NotifyListAPMIsRead", data);
      const notifyListAPMIsRead = (data) => {
        apiNotifyListAPMIsRead(data)
          .then((res) => {
            const json = res.data;
            if (json.Status) {
              // apiHander.FunctionToken(getBackNotifyList, reqBasic.value);
            }
          })
          .catch((err) => {
            console.log(err);
          });
      }

      //====== 初始化
      onBeforeMount(() => {
        if(states.uaertype == 0) {
          apiHander.FunctionToken(getReviewNotifyList, reqBasic.value);
        }
        else if(states.uaertype == 1)
          apiHander.FunctionToken(getBackNotifyList, reqBasic.value);
      })
      
      //======= public
      return{
        test,
        handleLogout,
        states,
        usageListInfo,
        NotifyList,
        isReadCount,
        notifiesModalInfo,
        notifiesModalClose,
        APMIsRead,
        dateConvert,
      }
    }
  })
</script>