import { createRouter, createWebHashHistory } from "vue-router";
import Home from "../view/Home.vue";
import Login from "../view/Login.vue";
import Agree from "../view/Party/Agree.vue";
import Description from "../view/Party/Description.vue";
import Inheritance from "../view/Party/Inheritance.vue";
import DataType from "../view/Party/DataType.vue";
import DataUsage from "../view/Party/DataUsage.vue";
import PlanInfo from "../view/User/PlanInfo.vue";
import PlanUsage from "../view/User/PlanUsage.vue";
import PlanContext from "../view/User/PlanContext.vue";
import PlanMatch from "../view/User/PlanMatch.vue";

// import Login2 from "../view2/Login.vue";
// import Agree2 from "../view2/Agree.vue";
// import Home2 from "../view2/Home.vue";
// import DataUsage2 from "../view2/DataUsage.vue";
// import DataType2 from "../view2/DataType.vue";

const routes = [
	{
		path: "/",
		name: "Home",
		component: Home,
	},
	{
		path: "/login",
		name: "Login",
		component: Login,
	},
	{
		path: "/agree",
		name: "Agree",
		component: Agree,
	},
	{
		path: "/description",
		name: "Description",
		component: Description,
	},
	{
		path: "/inheritance",
		name: "Inheritance",
		component: Inheritance,
	},
	{
		path: "/dataType",
		name: "DataType",
		component: DataType,
	},
	{
		path: "/dataUsage",
		name: "DataUsage",
		component: DataUsage,
	},
	{
		path: "/planInfo",
		name: "PlanInfo",
		component: PlanInfo,
	},
	{
		path: "/planUsage",
		name: "PlanUsage",
		component: PlanUsage,
	},
	{
		path: "/planContext",
		name: "PlanContext",
		component: PlanContext,
	},
	{
		path: "/planMatch",
		name: "PlanMatch",
		component: PlanMatch,
	},

	// {
	// 	path: "/v2",
	// 	name: "Home2",
	// 	component: Home2,
	// },
	// {
	// 	path: "/login2",
	// 	name: "Login2",
	// 	component: Login2,
	// },
	// {
	// 	path: "/agree/v2",
	// 	name: "Agree2",
	// 	component: Agree2,
	// },
	// {
	// 	path: "/dataType/v2",
	// 	name: "DataType2",
	// 	component: DataType2,
	// },
	// {
	// 	path: "/dataUsage/v2",
	// 	name: "DataUsage2",
	// 	component: DataUsage2,
	// },
	// {
	// 	path: "/planInfo/v2",
	// 	name: "PlanInfo2",
	// 	component: PlanInfo,
	// },
	// {
	// 	path: "/planUsage/v2",
	// 	name: "PlanUsage2",
	// 	component: PlanUsage,
	// },
];
const router = createRouter({
	history: createWebHashHistory(),
	routes,
});
export default router;
