<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span style="font-weight: 900; font-size: large">文章列表</span>
      </div>
      <!-- 搜索区域 -->
      <div class="search-box">
        <el-input
          v-model="keyword"
          placeholder="请输入关键字"
          style="width: 300px"
        ></el-input>
        <!-- <el-form-item label="发布状态" style="margin-left: 15px">
            <el-select v-model="q.state" placeholder="请选择状态" size="small">
              <el-option label="已发布" value="已发布"></el-option>
              <el-option label="草稿" value="草稿"></el-option>
            </el-select>
          </el-form-item> -->
        <el-button
          type="primary"
          size="small"
          @click="getCateList"
          style="margin-left: 0.8%"
          >搜索</el-button
        >
        <el-button type="info" size="small" @click="resetForm">重置</el-button>
        <!-- 发表文章的按钮 -->
        <el-button
          type="primary"
          size="small"
          class="btn-pub"
          @click="addArticle()"
          >发表文章</el-button
        >
      </div>

      <!-- 文章表格区域 -->
      <el-table :data="CateList" style="width: 100%" border>
        <el-table-column prop="title" label="标题">
          <template v-slot="{ row }">
            <el-link type="primary" @click="ArticleDetails(row.id)">{{
              row.articleTitle
            }}</el-link>
          </template>
        </el-table-column>
        <el-table-column prop="cate_name" label="文章分类">
          <template v-slot="{ row }">{{ row.appArticleClassName }}</template>
        </el-table-column>
        <el-table-column prop="pub_date" label="发布时间">
          <template v-slot="{ row }">{{ FormDate(row.pub_date) }}</template>
        </el-table-column>
        <el-table-column prop="state" label="状态"> </el-table-column>
        <el-table-column label="操作">
          <template v-slot="{ row }">
            <el-button type="danger" size="mini" @click="deleteArt(row.id)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="q.pageIndex"
        :page-sizes="[5, 10, 15, 20]"
        :page-size="q.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
      >
      </el-pagination>
    </el-card>
  </div>
</template>

<script>
import dayjs from "dayjs";
import defaultImg from "../../../assets/images/cover.jpg";
import { time } from "echarts";
import { getArticles, deleteArticles } from "@/api/articleApi";
export default {
  name: "ArtList",
  inject: ["reload"],
  data() {
    return {
      keyword: "",
      // 查询参数对象
      q: {
        pageIndex: 1,
        pageSize: 10,
        keyword: "",
      },

      // 点击文章详情开关
      artDetailsVisible: false,
      total: 0,
      // 新增文章存储数据的对象
      pubForm: {
        title: "",
        content: "",
        cover_img: null,
      },
      // 存放文章详情的对象
      ArtDetail: {},
      // 页面文章列表
      CateList: [],
      // 新增文章表单校验规则
      pubFormRules: {
        title: [
          { required: true, message: "请输入文章标题", trigger: "blur" },
          {
            min: 1,
            max: 30,
            message: "长度在 1 到 30 个字符",
            trigger: "blur",
          },
        ],
        content: [
          { required: true, message: "请输入文章内容", trigger: "blur" },
        ],
      },
      // 存储文章分类列表
      ArtList: [],
    };
  },
  methods: {
    handleSizeChange(val) {
      // console.log(`每页 ${val} 条`);
      this.q.pageSize = val;
      this.fetchData();
      // this.reload();
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
      this.q.pageIndex = val > 0 ? val : 1;
      console.log(this.q);
      this.fetchData();
    },
    // 拉取后台数据（带分页）
    fetchData() {
      let params = {
        pageIndex: this.q.pageIndex,
        pageSize: this.q.pageSize,
        keyword: this.q.keyword,
      };
      console.log(params);
      getArticles(params).then((res) => {
        console.log(res);
        this.CateList = res.data.data;
        this.total = res.data.pageData.total;
        this.q = res.data.pageData;
      });
    },

    addArticle() {
      this.$router.push("/release");
    },

    // 初始化的时候获取页面文章的函数
    async getCateList() {
      this.$message.success("查询成功");
      // console.log(this.keyword);
      this.q.keyword = this.keyword;
      // console.log(this.q);
      this.fetchData();
      // this.CateList = res.data;
      // this.total = res.total;
    },
    // 格式化时间
    FormDate(time) {
      return dayjs(time).format(" YYYY-MM-DDHH:mm:ss");
    },
    // 点击重置按钮重新加载
    resetForm() {
      this.keyword = "";
    },
    // 点击删除对应的文章
    deleteArt(id) {
      this.$confirm("此操作将永久删除该文章, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          deleteArticles(id).then((res) => {
            if (res.code == 20003) {
              this.$message.success("删除成功");
              this.reload();
            } else {
              this.$message.warning("删除失败");
            }
          });
        })
        .catch(() => {
          this.$message.info("已取消删除");
        });
    },
  },
  created() {
    this.fetchData()
  },
};
</script>

<style lang="less" scoped>
.search-box {
  margin-bottom: 5px;
  display: flex;
  // justify-content: space-between;
  align-items: stretch;
  // .btn-pub {
  //   margin-top: 5px;
  // }
}
/deep/ .ql-editor {
  height: 300px;
}
</style>
