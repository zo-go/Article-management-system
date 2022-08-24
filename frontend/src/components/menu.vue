<template>
  <el-header>
    <div class="Header">
      <div>
        <el-dropdown :hide-on-click="false" class="First">
          <el-dropdown-menu> </el-dropdown-menu>
          <el-button
            type="text"
            @click="goMain"
            style="
              font-weight: 600;
              font-size: larger;
              margin-left: -12%;
              padding-left: 5%;
            "
            icon="el-icon-s-home"
            >首页</el-button
          >
        </el-dropdown>
        <el-dropdown
          :hide-on-click="false"
          style="margin-top: -0.06%; margin-left: 1.5%"
        >
          <span
            class="el-dropdown-link"
            style="font-weight: 600; font-size: larger"
          >
            分类
          </span>
          <el-dropdown-menu slot="dropdown">
            <div v-for="item in ArticleClass" :key="item.Id">
              <el-dropdown-item>{{
                item.AppArticleClassName
              }}</el-dropdown-item>
            </div>
          </el-dropdown-menu>
        </el-dropdown>
        <el-dropdown
          :hide-on-click="false"
          class="mine"
          style="margin-left: 1.5%"
        >
          <el-dropdown-menu></el-dropdown-menu>
          <el-button
            type="text"
            @click="goMeInfo"
            style="font-weight: 600; font-size: larger"
            icon="el-icon-s-custom"
            >我的</el-button
          >
        </el-dropdown>
      </div>
      <div class="search">
        <el-input
          v-model="keyword"
          placeholder="请输入内容"
          class="substance"
        ></el-input>
        <el-button
          type="text"
          icon="el-icon-search"
          class="seek"
          @click="query(keyword)"
          style="margin-left: 0.8%; font-weight: 500; font-size: larger"
          >搜索
        </el-button>
      </div>
      <div>
        <span class="login" v-if="this.userInfo">
          <el-menu
            class="el-menu-top"
            mode="horizontal"
            text-color="black"
            active-text-color="#409EFF"
            default-active="#fffff"
          >
            <el-menu-item index="2" class="creation">
              <router-link :to="{ path: '/release' }">
                <i
                  class="el-icon-edit"
                  style="
                    margin-left: -500%;
                    border-top: 3.5px double rgb(97, 208, 233);
                    border-bottom: 3.5px double rgb(239, 188, 188);
                    border-left: 3.5px double rgb(198, 164, 223);
                    border-radius: 12%;
                  "
                >
                  创作中心
                </i>
              </router-link>
            </el-menu-item>
            <el-submenu index="1">
              <template slot="title">
                <!-- 头像 -->
                <img
                  style="width: 50px; height: 50px"
                  :src="this.avatar"
                  alt=""
                  class="avatar"
                  v-if="this.avatar"
                />
                <img src="@/assets/logo.png" alt="" class="avatar" v-else />
                <span
                  style="font-weight: 600; font-size: larger"
                  icon="el-icon-s-data"
                  >个人中心</span
                >
              </template>
              <el-menu-item index="1-1" @click="BasicInfon"
                ><i class="el-icon-s-operation"></i>个人资料</el-menu-item
              >
              <el-menu-item index="4" @click="PersonalBackground"
                ><i class="el-icon-s-operation"></i>个人后台</el-menu-item
              >
              <el-menu-item index="3" @click="logout"
                ><i class="el-icon-switch-button"></i>退出</el-menu-item
              >
            </el-submenu>
          </el-menu>
        </span>
        <span class="login" v-else>
          <el-button type="text" @click="goLogin">登录</el-button>
          <el-button type="text" @click="goReg">注册</el-button>
        </span>
      </div>
    </div>
  </el-header>
</template>


<script>
import baseUrl from "@/config/appConfig.js";
import { getArticles } from "@/api/articleApi";
import { getArticleClass } from "@/api/articleClassApi";
export default {
  name: "Menu",
  data() {
    return {
      oldScrollTop: 0,
      isHide: false,
      keyword: "",
      avatar: "",
      ArticleClass: "",
      userInfo: this.$store.state.userInfo.Username,
    };
  },
  created() {
    var avatarUrl = this.$store.state.userInfo.Avatar;
    this.avatar = baseUrl.baseUrl + "/Files/" + avatarUrl;
    getArticleClass().then((res) => {
      this.ArticleClass = res.data;
    });
  },
  methods: {
    query() {
      var keyword = this.keyword.replace(" ", "");
      this.$emit("func", keyword);
    },
    // 退出登录
    logout() {
      // 询问用户是否退出登录
      //confirm可更改文字 提示样式 confrim('文字' , type:""(提示类型),title:""(提示框标题文字),cancelButtonText:''(取消按钮文字内容)，confirmButtonText:''(按钮文字内容))
      this.$confirm("你确定退出吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        // console.log(localStorage.getItem('accessToken'));
        // console.log(this.$store.state.token);
        // localStorage.getItem('accessToken') = "";
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("vuex");
        this.$store.state.token = "";
        this.$router.push("/login");
      });
    },
    BasicInfon() {
      this.$router.push("/personalCenter");
    },
    // 前往个人后台
    PersonalBackground() {
      this.$router.push("/home");
    },
    // 前往首页
    goMain() {
      this.$router.push("/cmsmain");
    },
    // 前往用户中心
    goMeInfo() {
      this.$router.push("/personalCenter");
    },
    goLogin() {
      this.$router.push("/login");
    },
    goReg() {
      this.$router.push("/reg");
    },
  },
};
</script>


<style lang="less" scoped>
.avatar {
  border-radius: 50%;
  width: 35px;
  height: 35px;
  background-color: #fff;
  margin-right: 10px;
  object-fit: cover;
}
.el-header,
.el-footer {
  background-color: white;
  color: #d3dce6;
  text-align: center;
  line-height: 50px;
  height: 50px;
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
  width: 100%;
  background-size: 100% 100%;
  position: absolute;
  background-color: #fff;
  background-image: url(@/assets/images/background.png);
}
.home-fixed .el-link {
  font-size: 25px;
  float: right;
}
.home-fixed {
  width: 100%;
  height: 104px;
  position: fixed;
  left: 0;
  top: 0;
  z-index: 999;
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
  transition: all 0.3s;
}
.el-header {
  width: 100%;
}
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
.el-autocomplete {
  float: left;
}
.home-fixed[data-v-54f97f15] {
  height: 20px;
}
.element.style {
  height: 30px !important;
}
.search {
  float: left;
  margin-left: 10px;
  width: 250px;
}
.login {
  float: right;
}
.substance {
  width: 175px;
}

// 创作
.creation {
}
</style>