<template>
  <el-container class="main-container">
    <!-- 头部区域 -->
    <el-header>
      <!-- 左侧的 logo -->
      <a href="https://art.zbmwd.top/#/cmsmain"
        ><img src="@/assets/images/login_title.png" alt=""
      /></a>
      <!-- 右侧的菜单 -->
      <el-menu
        class="el-menu-top"
        mode="horizontal"
        background-color="#23262E"
        text-color="#fff"
        active-text-color="#409EFF"
      >
        <el-submenu index="1">
          <template slot="title">
            <!-- 头像 -->
            <img :src="this.avatar" alt="" class="avatar" v-if="this.avatar" />
            <img src="../../assets/logo.png" alt="" class="avatar" v-else />
            <span>个人中心</span>
          </template>
          <el-menu-item index="1-1" @click="BasicInfon"
            ><i class="el-icon-s-operation"></i>基本资料</el-menu-item
          >
        </el-submenu>
        <el-menu-item index="2" @click="logout"
          ><i class="el-icon-switch-button"></i>退出</el-menu-item
        >
      </el-menu>
    </el-header>
    <el-container>
      <!-- 侧边栏区域 -->
      <el-aside width="200px">
        <div class="user-box">
          <img :src="this.avatar" alt="" v-if="this.avatar" />
          <img src="../../assets/logo.png" alt="" v-else />
          <span>欢迎 {{ userInfo.Nickname || userInfo.Username }}</span>
        </div>

        <el-menu
          :default-active="$route.path"
          class="el-menu-vertical-demo"
          background-color="#23262E"
          text-color="#fff"
          active-text-color="#409EFF"
          unique-opened
          router
        >
          <template v-for="item in menuList">
            <!-- 不包含子菜单的“一级菜单” -->
            <el-menu-item
              :index="item.indexPath"
              :key="item.indexPath"
              v-if="!item.children"
              ><i :class="item.icon"></i>{{ item.title }}</el-menu-item
            >
            <!-- 包含子菜单的“一级菜单” -->
            <el-submenu
              :index="item.indexPath"
              :key="item.children.indexPath"
              v-else
            >
              <template slot="title">
                <i :class="item.icon"></i>
                <span>{{ item.title }}</span>
              </template>
              <el-menu-item
                :index="childItem.indexPath"
                v-for="childItem in item.children"
                :key="childItem.indexPath"
                ><i :class="childItem.icon"></i
                >{{ childItem.title }}</el-menu-item
              >
            </el-submenu>
          </template>
        </el-menu>
      </el-aside>
      <el-container>
        <el-main> <router-view></router-view> </el-main>
        <el-footer></el-footer>
      </el-container>
    </el-container>
  </el-container>
</template>
<script>
import { mapState } from "vuex";
import { getUserInfo } from "@/api/userApi";
import baseUrl from "@/config/appConfig";
export default {
  name: "Main",
  computed: {
    ...mapState(["userInfo", "token"]),
  },
  data() {
    return {
      menuList: [
      ],
      filesUrl: baseUrl.baseUrl + "/files/",
      avatar: "",
    };
  },
  methods: {
    BasicInfon() {
      this.$router.push("/user-info");
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
    async getMunuList() {
      // const { data: res } = await this.$http.get("/my/menus");
      // // console.log(res);
      // if (res.code === 0) {
      //   this.menuList = res.data;
      // }
    },
    setToken() {
      const token = localStorage.getItem("accessToken");
      // console.log(localStorage.getItem("accessToken"));
      if (!token) {
        this.$router.push("/login");
      }
    },
    // getUser() {
    //   getUserInfo("admin").then((res) => {
    //     // console.log(res);
    //   });
    // },
  },
  created() {
    this.avatar = this.filesUrl + this.userInfo.Avatar;
    // console.log(this.avatar);
    this.menuList = this.$store.state.menu
    // console.log(this.userInfo);
    if (!this.token) {
      this.$router.push("/login");
    }
    // console.log(this.userInfo);
  },
};
</script>

<style lang="less" scoped>
.main-container {
  height: 100%;
  .el-header,
  .el-aside {
    background-color: #23262e;
  }
  .el-header {
    padding: 0;
    display: flex;
    justify-content: space-between;
  }
  .el-main {
    overflow-y: scroll;
    height: 0;
    background-color: #f2f2f2;
  }
  .el-footer {
    background-color: #eee;
    font-size: 12px;
    display: flex;
    justify-content: center;
    align-items: center;
  }
}

.avatar {
  border-radius: 50%;
  width: 35px;
  height: 35px;
  background-color: #fff;
  margin-right: 10px;
  object-fit: cover;
}
.user-box {
  height: 70px;
  display: flex;
  justify-content: center;
  align-items: center;
  border-top: 1px solid #000;
  border-bottom: 1px solid #000;
  user-select: none;
  img {
    width: 35px;
    height: 35px;
    border-radius: 50%;
    background-color: #fff;
    margin-right: 15px;
    object-fit: cover;
  }
  span {
    color: white;
    font-size: 12px;
  }
}
.el-aside {
  .el-submenu,
  .el-menu-item {
    width: 200px;
    user-select: none;
  }
}
</style>
