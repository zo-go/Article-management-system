import request from "@/utils/request";

// 获取文章评论
export function getArticleComment(){
    return request.get("/ArticleComment");
}

// 根据id查找
export async function getArticleCommentById(id) {
    return await request.get(`/ArticleComment/${id}`)
}

// 添加文章评论
export function addArticleComment(data) {
    return request.post("/ArticleComment", data)
}

// 删除文章评论
export function deleteArticleComment(id){
    return request.delete(`/ArticleComment/${id}`)
}