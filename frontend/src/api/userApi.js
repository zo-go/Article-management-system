import request from "@/utils/request";
import { getToken, getRefreshToken } from '@/utils/auth'

// 登录并获得token
export function login(data) {
    return request.post("/users/token", data);
}

// 不登录的情况下刷新token
export function refreshToken() {
    let accessToken = getToken();
    let refreshToken = getRefreshToken();
    let data = {
        accessToken,
        refreshToken
    }
    if (accessToken && refreshToken) {
        return request.post("/users/refreshtoken", data);
    } else {
        return false;
    }
}

// 获取用户
export function getUsersInfo(params){
    return request.get("/users",{ params: params })
}

// 注册用户
export function regUser(data) {
    return request.post("/users", data)
}

// 根据用户名获取指定用户
export function getUserInfo(uesrname){
    return request.get(`/users/${uesrname}`)
}

// 修改用户信息
export function updateUserInfo(id,data){
    return request.put(`/users/${id}`,data)
}

export function deleteUser(id){
    return request.delete(`/users/${id}`)
}