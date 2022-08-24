import request from "@/utils/request";

// 获取文章
export async function getArticles(params) {
    return await request.get("/blogs", { params: params });
}

// 添加文章
export async function addArticle(data) {
    return await request.post("/blogs", data)
}

// 修改文章信息
export async function updateArticleInfo(id, data) {
    return await request.put(`/blogs/${id}`, data)
}

// 删除文章
export async function deleteArticles(id) {
    return await request.delete(`/blogs/${id}`)
}

// 根据id查找
export async function getArticlesId(id) {
    return await request.get(`/blogs/${id}`)
}