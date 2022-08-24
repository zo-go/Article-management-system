import axios from 'axios'
import { getToken } from '@/utils/auth'
import appConfig from '@/config/appConfig'
import store from '@/store/index'


const instance = axios.create({
    baseURL: appConfig.baseUrl,
    timeout: 50000
});


// 添加请求拦截器
instance.interceptors.request.use(
    function (config) {
        // 在发送请求之前做些什么
        // store.state.token = getToken()
        let token = getToken();
        if (token) {
            config.headers['Authorization'] = 'Bearer ' + token
        }

        return config;
    }, function (error) {
        // 对请求错误做些什么
        return Promise.reject(error);
    });

// 添加响应拦截器
instance.interceptors.response.use(function (response) {
    // 对响应数据做点什么
    return response.data;
}, function (error) {
    // 对响应错误做点什么
    return Promise.reject(error);
});

export default instance