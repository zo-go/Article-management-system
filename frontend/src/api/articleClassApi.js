import request from "@/utils/request";

// 获取文章分类
export function getArticleClass(){
    return request.get("/ArticleClass");
}

// 添加文章分类
export function addArticleClass(data) {
    return request.post("/ArticleClass", data)
}

// 修改文章信息
export function updateArticleClassInfo(id,data){
    return request.put(`/ArticleClass/${id}`,data)
}

// 删除文章分类
export function deleteArticleClass(id){
    return request.delete(`/ArticleClass/${id}`)
}