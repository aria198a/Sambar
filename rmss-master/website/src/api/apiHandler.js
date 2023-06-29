import axios from 'axios';

export function apihandler() {


    var host = "";
    var base = "";
    if(import.meta.env.DEV){
        host = "/api/api";
        base = "/api";
    }

    if(import.meta.env.PROD){
        host = "/api/api";
        base = "/api";
        // host = "/rms/api/api";
        // base = "/rms/api";
    }

    const HostURL = host;
    const userRequest =  axios.create({ baseURL: base });

    // const HostURL = "/api/api";
    // const userRequest = axios.create({ baseURL: "/api" });

    // const HostURL = "/rms/api/api";
    // const userRequest = axios.create({ baseURL: "/rms/api" });

    const urls = {
        getToken: `${HostURL}/User/Token`,
    };

    const _sessionData = (key, val = undefined) => {
        if (val == undefined) {
            return sessionStorage.getItem(key);
        } else if (val == "") {
            sessionStorage.removeItem(key);
        } else {
            sessionStorage.setItem(key, val);
        }
    }
    const FunctionToken = (EXfunction, data_in) => {
        axios.post(urls.getToken)
            .then((res) => {

                // data_in.TokenID = this.TokenID;
                console.log(JSON.stringify(data_in));
                let TokenID = _sessionData("TokenID");
                let TokenID2 =
                    "A9207nM9qUOQMqKmaFNxmwdLAxc3WN+W54GRH9EIzjVMNT1kuJoXLzXOnqhYmRu32KRMzdcbanDLYGjTNfEOcw==";
                // console.log(TokenID, TokenID2);
                if (TokenID == null) {
                    // if (this.IsNull(TokenID) == "") {
                    data_in.TokenID = TokenID2;
                } else {
                    // console.log(data_in);
                    data_in.TokenID = TokenID;
                }
                data_in.Token = res.data;
                data_in.Page = "";

                EXfunction(data_in);

            })
            .catch((error) => {

                alert("網路連線發生異常，請檢查API是否啟用"+error)
                //  alert("發生錯誤FunctionToken!" + error);
            });
    }

    return {
        FunctionToken,
        userRequest,
        _sessionData
    }
}