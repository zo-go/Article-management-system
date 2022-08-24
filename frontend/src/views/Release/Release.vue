<template>
  <el-container>
    <div class="bjimg">
      <!-- <menu-conpent></menu-conpent> -->
      <el-container>
        <el-aside width="200px">
          <div class="LeftCenter">
            <div class="BlogBackground">
              <h3>博客后台</h3>
              <ul>
                <li>
                  <el-link type="warning">新建随笔</el-link>
                </li>
                <li>
                  <el-link type="danger">草稿箱</el-link>
                </li>
                <li>
                  <el-link type="info">回收站</el-link>
                </li>
              </ul>
            </div>
            <div>
              <h3>分类</h3>
              <ul>
                <li>
                  <el-link type="warning">新建分类</el-link>
                </li>
                <li>
                  <el-link type="danger">分类列表</el-link>
                </li>
                <li>
                  <el-link type="info">未分类</el-link>
                </li>
              </ul>
            </div>
          </div>
        </el-aside>
        <el-main>
          <template>
            <!-- 发布文章的对话框 -->

            <el-form
              :model="pubForm"
              :rules="pubFormRules"
              ref="pubFormRef"
              label-width="100px"
            >
              <el-form-item label="文章标题" prop="ArticleTitle">
                <el-input
                  v-model="pubForm.ArticleTitle"
                  placeholder="请输入标题"
                ></el-input>
              </el-form-item>
              <el-form-item label="文章分类" prop="Id">
                <el-select
                  v-model="pubForm.Id"
                  placeholder="请选择分类"
                  style="width: 100%"
                >
                  <el-option
                    v-for="item in ArtList"
                    :label="item.AppArticleClassName"
                    :value="item.Id"
                    :key="item.Id"
                  ></el-option>
                </el-select>
              </el-form-item>
              <!-- 文章内容编辑 -->
              <!-- <el-form-item label="文章内容" prop="ArticleContent">
                <quill-editor
                  v-model="pubForm.ArticleContent"
                  ref="myQuillEditor"
                ></quill-editor>
              </el-form-item> -->
              <el-form-item label="文章内容" prop="ArticleContent">
                <mavon-editor
                  v-model="pubForm.ArticleContent"
                  ref="md"
                  @imgAdd="$imgAdd"
                  @change="change"
                  style="min-height: 600px"
                />
              </el-form-item>

              <el-form-item label="文章封面">
                <el-upload
                  class="avatar-uploader"
                  :action="actionUrl"
                  :headers="actionHeader"
                  :show-file-list="false"
                  :on-success="handleAvatarSuccess"
                  :before-upload="beforeAvatarUpload"
                >
                  <img v-if="imageUrl" :src="imageUrl" class="avatar" />
                  <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                </el-upload>
                <!-- 选择封面的按钮 -->
                <!-- <el-button type="text" @click="$refs.iptFile.click()"
                  >+ 选择封面</el-button
                > -->
              </el-form-item>

              <el-form-item>
                <el-button type="primary" @click="addArtList('已发布')"
                  >发布</el-button
                >
                <!-- <el-button type="info" @click="b('草稿')">存为草稿</el-button> -->
                <el-button type="info" @click="Unpublish('取消发布')"
                  >取消发布</el-button
                >
              </el-form-item>
            </el-form>
          </template>
        </el-main>
      </el-container>
    </div>
  </el-container>
</template>

<script>
import dayjs from "dayjs";
// import defaultImg from "@/assets/images/cover.jpg";
import { getArticleClass } from "@/api/articleClassApi";
import { addArticle } from "@/api/articleApi";
// import MenuConpent from "@/components/menu.vue";
import baseUrl from "@/config/appConfig";
// import { time } from "echarts";
import { getToken } from "@/utils/auth";

//markdown组件
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";

export default {
  name: "Release",
  components: {
    // MenuConpent,
    mavonEditor,
  },
  data() {
    return {
      content: "", // 输入的markdown
      html: "", // 及时转的html
      configs: {},
      actionHeader: { Authorization: "Bearer " + getToken() }, // token
      actionUrl: baseUrl.baseUrl + "/files", // token传递地址
      oldScrollTop: 0,
      isHide: false,
      // 新增文章存储数据的对象
      pubForm: {
        ArticleTitle: "",
        ArticleContent: "",
        Document: "",
        AppArticleClassId: "",
        Id: "",
        AppArticleClassName: "",
      },
      pubFormRules: {
        ArticleTitle: [
          { required: true, message: "请输入文章标题", trigger: "blur" },
          {
            min: 1,
            max: 30,
            message: "长度在 1 到 30 个字符",
            trigger: "blur",
          },
        ],
        Id: [{ required: true, message: "请选择文章分类", trigger: "change" }],
        ArticleContent: [
          { required: true, message: "请输入文章内容", trigger: "blur" },
        ],
      },
      // 存储文章分类列表
      ArtList: [],
      imageUrl: "",
      cover: "",
    };
  },
  mounted() {
    // 监听页面滚动事件
    window.addEventListener("scroll", this.scrolling);
  },
  methods: {
    $imgAdd(pos, $file) {
      let formdata = new FormData();

      this.$upload
        .post(baseUrl.baseUrl+"/files/", formdata)
        .then((res) => {
          console.log(res.data);
          this.$refs.md.$img2Url(pos, res.data);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    change(value, render) {
      // render 为 markdown 解析后的结果[html]
      console.log(render);
      this.html = render;
    },
    // 取消发布并返回主页
    Unpublish() {
      this.$router.push("/cmsmain");
    },
    // 选取封面图片
    handleAvatarSuccess(res, file) {
      this.cover = res.data[0];
      this.$store.state.userInfo.Avatar = res.data[0];
      this.imageUrl = `${baseUrl.baseUrl}/files/${res.data[0]}`;
    },

    // 图片上传的规则
    beforeAvatarUpload(file) {
      const isJPG = file.type === "image/jpeg";
      const isLt2M = file.size / 1024 / 1024 < 5;

      if (!isJPG) {
        this.$message.error("上传图片只能是 JPG,jpeg 格式!");
      }
      if (!isLt2M) {
        this.$message.error("上传图片大小不能超过 5MB!");
      }
      return isJPG && isLt2M;
    },
    // 添加文章并发布 上传数据给后端
    addArtList() {
      this.$confirm("确定保存并发布文章?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "info",
      })
        .then(() => {
          var pubData = {
            ArticleTitle: this.pubForm.ArticleTitle,
            // ArticleContent: this.pubForm.ArticleContent,
            ArticleContent: this.html,
            Document: this.cover,
            AppArticleClassId: this.pubForm.Id,
            AppUserId: this.$store.state.userInfo.Id,
          };
          console.log(pubData);
          addArticle(pubData).then((res) => {
            console.log(res);
            if ((res.code = 20001)) {
              this.$message({
                type: "success",
                message: "发布成功!",
              });
              this.$router.push("/cmsmain");
            } else {
              this.$message({
                type: "error",
                message: "出现了错误,发布失败!",
              });
            }
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消发布!",
          });
        });
    },
    scrolling() {
      // 滚动条距文档顶部的距离（做兼容处理）
      let scrollTop =
        window.pageYOffset ||
        document.documentElement.scrollTop ||
        document.body.scrollTop;
      // 滚动条滚动的距离
      let scrollStep = scrollTop - this.oldScrollTop;
      // 更新——滚动前，滚动条距文档顶部的距离
      this.oldScrollTop = scrollTop;
      if (scrollStep < 0) {
        this.isHide = false;
      } else {
        this.isHide = true;
      }
    },
    // 选取图片
    // chooseImg(e) {
    //   const files = e.target.files;
    //   if (files.length > 0) {
    //     this.pubForm.Document = files[0];
    //     const url = URL.createObjectURL(files[0]);
    //     this.$refs.imgRef.setAttribute("src", url);
    //   } else {
    //     this.pubForm.Document = null;
    //     this.$refs.imgRef.setAttribute("src", defaultImg);
    //   }
    // },
    // 格式化时间
    FormDate(time) {
      return dayjs(time).format(" YYYY-MM-DDHH:mm:ss");
    },
  },
  created() {
    // getUserInfo().then((res) => {
    //   console.log(res);
    // });
    // 获取文章分类
    console.log(this.$store.state.userInfo);
    if (!this.$store.state.token) {
      this.$message.info("请先登录");
      this.$router.push("/login");
    }
    getArticleClass().then((res) => {
      console.log(res);
      this.ArtList = res.data;
      console.log(this.ArtList);
    });
  },
};
</script>

<style lang="less" scoped>
.el-header,
.el-footer {
  background-color: white;
  color: #d3dce6;
  text-align: center;
  line-height: 60px;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}

.el-aside {
  /* background-color:rgba(235, 232, 232, 0.89); */
  color: black;
  text-align: center;
  line-height: 30px;
}

.el-main {
  color: black;
  text-align: center;
  height: 100vh;
}

body > .el-container {
  margin-bottom: 40px;
}

.el-container:nth-child(5) .el-aside,
.el-container:nth-child(6) .el-aside {
  line-height: 260px;
}

.el-container:nth-child(7) .el-aside {
  line-height: 320px;
}

.el-link {
  float: left;
}

.bjimg {
  /* position: fixed; */
  /* top: 0; 
      left: 0;  */
  width: 100%;
  background-size: 100% 100%;
  position: absolute;
  /* min-width: 1000px;
      z-index: -10;
      zoom: 1; */
  background-color: #fff;
  background-image: url(@/assets/images/background.png);
  /* background-repeat: no-repeat;
      background-size: cover;
      -webkit-background-size: cover;
      -o-background-size: cover;
      background-position: center 0; */
}

.home-fixed .el-link {
  font-size: 25px;
  float: right;
}

.home-fixed {
  width: 100%;
  height: 104px;
  /* background: #fff; */
  position: fixed;
  left: 0;
  top: 0;
  z-index: 999;
  /* box-shadow: 0px 3px 7px 0px rgba(70, 70, 70, 0.35);
    background: #fff; */
  transition: all 0.3s;
}

.home-fixed.isHide {
  top: -104px;
}

.home-fixed {
  width: 100%;
  height: 50px;
  background: #fff;
  position: fixed;
  left: 0;
  top: 0;
  z-index: 999;
  /* box-shadow: 0px 3px 7px 0px rgba(70, 70, 70, 0.35);
  background: #fff; */
  transition: all 0.3s;
}

/* .el-header {
  background: url(@/assets/images/cover.jpg);
} */

.home-fixed.isHide {
  top: -75px;
}

.el-dropdown {
  vertical-align: top;
  position: relative;
  color: black;
}

.el-dropdown + .el-dropdown {
  margin-left: 10px;
}

.el-icon-arrow-down {
  font-size: 12px;
}

.el-dropdown {
  float: left;
}

.el-dropdown.item {
  float: left;
  width: 126px;
}

.el-dropdown-menu__item {
  float: left;
}

.el-button--text {
  float: right;
  line-height: 60px;
  padding-left: 5px;
  padding-top: 0px;
  font-size: 14px;
  margin: 0 0 0 5px;
  color: black;
}

.Header {
  line-height: 60px;
  padding-left: 5px;
  padding-top: 0px;
  font-size: 14px;
  margin: 0 0 0 5px;
}

.mine {
  bottom: 1px;
  right: 10px;
}
.First {
  bottom: 1px;
}
</style>