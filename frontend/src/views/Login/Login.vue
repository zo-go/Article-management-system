<template>
  <div class="login-container">
    <div class="a">
      <div class="b"></div>
      <div class="c"></div>
      <div class="d"></div>
      <div class="e"></div>
      <div class="f">
        <div
          class="login-box"
          style="box-shadow: 5px 3px 5px 4px rgba(27, 26, 26, 0.3)"
        >
          <div class="title-box">Login</div>
          <div class="demo-image">
            <el-image
              style="
                width: 300px;
                height: 200px;
                border-radius: 8px;
                box-shadow: 2px 4px 6px 6px rgba(27, 26, 26, 0.3);
              "
              :src="require('@/assets/images/img (1).jpg')"
            ></el-image>
          </div>
          <el-form
            :model="loginForm"
            :rules="loginRules"
            ref="loginForm"
            class="login"
          >
            <div class="avatar-image">
              <el-image
                style="
                  width: 100px;
                  height: 100px;
                  border-radius: 100%;
                  border-left: 3px solid rgba(255, 255, 255, 0.5);
                  border-bottom: 3px solid rgba(255, 255, 255, 0.3);
                  border-right: 1px solid rgba(255, 255, 255, 0.3);
                  box-shadow: 2px 4px 5px 5px rgba(26, 25, 25, 0.2);
                "
                :src="require('@/assets/images/login_avatar.jpg')"
              ></el-image>
            </div>
            <el-form-item prop="username">
              <el-input
                class="loginForm"
                v-model="loginForm.username"
                placeholder="请输入用户名"
                maxlength="10"
                minlength="1"
              ></el-input>
            </el-form-item>
            <!-- 密码 -->
            <el-form-item prop="password" style="padding-top: 10px">
              <el-input
                class="loginForm"
                v-model="loginForm.password"
                type="password"
                placeholder="请输入密码"
                maxlength="15"
                minlength="6"
              ></el-input>
            </el-form-item>
            <el-link
              type="info"
              @click="$router.push('/reg')"
              class="btn-register"
              style="box-shadow: 2px 2px 1px 1px rgba(27, 26, 26, 0.1)"
              >注册</el-link
            >
            <div>
              <el-button
                type="primary"
                class="btn-login"
                @click="submitForm('loginForm')"
                style="box-shadow: 3px 3px 2px 2px rgba(27, 26, 26, 0.3)"
                >登录</el-button
              >
              <el-button
                @click="resetForm('loginForm')"
                class="btn-login"
                style="
                  box-shadow: 3px 3px 2px 2px rgba(27, 26, 26, 0.3);
                  margin-left: 125px;
                "
                >重置</el-button
              >
            </div>
          </el-form>
        </div>
      </div>
    </div>
    <div class="square-circle">
      <div class="square">
        <ul>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
        </ul>
      </div>
      <div class="circle">
        <ul>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import { login, getUserInfo } from "@/api/userApi";
import { setToken } from "@/utils/auth";
import store from "@/store";
import { getMenu } from "@/api/menu";
export default {
  name: "Login",
  data() {
    return {
      // 登录表单的数据对象
      loginForm: {
        username: "",
        password: "",
      },
      // 登录表单的验证规则对象
      loginRules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          {
            pattern: /^[a-zA-Z][a-zA-Z0-9]{0,9}$/,
            message: "用户名必须是1-10的字母数字,数字不能开头",
            trigger: "blur",
          },
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          {
            pattern: /^\S{6,15}$/,
            message: "密码必须是6-15的非空字符",
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          login(this.loginForm).then((res) => {
            console.log(res);
            if (res.code === 1000) {
              var menu = getMenu(this.loginForm).data.menu;
              this.$store.commit("clearMenu");
              this.$store.commit("setMenu", menu);
              setToken(res.data.AccessToken, res.data.RefreshToken);
              var token = localStorage.getItem("accessToken");
              this.$store.commit("setToken", token);
              getUserInfo(this.loginForm.username).then((res) => {
                // console.log(res);
                this.$store.commit("updataUserInfo", res.data);
              });
              // console.log(store);
              this.$router.push("/cmsmain");
            } else {
              this.$message({
                showClose: true,
                message: res.msg,
                type: "error",
              });
            }
          });
        } else {
          this.$message({
            showClose: true,
            message: "用户名或者密码不能为空，请确认重试",
            type: "error",
          });
          return false;
        }
      });
      // this.$refs.loginRef.validate(async (valid) => {
      //   if (!valid) return this.$message.error("登录失败");
      //   const { data: res } = await this.$http.post(
      //     "/api/login",
      //     this.loginForm
      //   );
      //   console.log(res);
      //   if (res.code !== 0) return this.$message.error(res.message);
      //   this.$message.success(res.message);
      //   this.$store.commit("setToken", res.token);

      //   this.$router.push("/");
      // });
    },
    // submitForm(formName) {
    //   this.$refs[formName].validate((valid) => {
    //     if (valid) {
    //       console.log(this.loginForm);
    //       var menu = getMenu(this.loginForm).data.menu;
    //       this.$store.commit("setMenu", menu);
    //     }
    //   });
    // },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
  },
  created() {},
};
</script>

<style lang="less" scoped>
.login-container {
  background: url("../../assets/images/login_bg2.0.jpg") center;
  background-size: cover;

  height: 100%;
  /* 100%窗口高度 */
  height: 100vh;
  /* 弹性布局 居中 */
  display: flex;
  justify-content: center;
  align-items: center;
  /* 溢出隐藏 */
  overflow: hidden;

  .login-box {
    border-left: 1px solid rgba(255, 255, 255, 0.5);
    border-bottom: 1px solid rgba(255, 255, 255, 0.3);
    border-right: 1px solid rgba(255, 255, 255, 0.3);
    backdrop-filter: blur(10px);
    background: rgba(50, 50, 50, 0.2);
    display: flex;
    border-radius: 10px;
    width: 700px;
    height: 366px;

    .square-circle {
      border-left: 3px solid rgba(255, 255, 255, 0.5);
      border-bottom: 3px solid rgba(255, 255, 255, 0.3);
      border-right: 1px solid rgba(255, 255, 255, 0.3);
    }

    .title-box,
    .demo-image {
      margin-left: 25px;
      position: absolute;
    }

    .demo-image {
      padding-top: 120px;
      float: right;
      border-radius: 10px;
    }

    .avatar-image {
      padding-bottom: 2%;
      padding-left: 30%;
    }

    .login {
      padding-top: 5%;
      padding-left: 52%;
      padding-bottom: 5%;
      width: 288px;
    }

    .title-box {
      text-transform: uppercase;
      color: #f1ebe5;
      text-shadow: 0 8px 9px #c4b59d, 0px -2px 1px #fff;
      font-weight: bold;
      letter-spacing: -4px;
      width: 190px;
      padding: 6px 0;
      margin-bottom: 30px;
      border: none;
      border-bottom: 1px solid rgb(223, 203, 203);
      outline: none;
      background: transparent;
      height: 71px;
      font-size: 57px;
      text-align: center;
      font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen,
        Ubuntu, Cantarell, "Open Sans", "Helvetica Neue", sans-serif;
    }

    .btn-login {
      position: absolute;
      padding-left: 1000px;
      letter-spacing: 3px;
      font-size: 15px;
      box-sizing: border-box;
      width: 88px;
      height: 35px;
      border-radius: 5px;
      border: 1px solid rgba(255, 255, 255, 0.5);
      background: rgba(255, 255, 255, 0.2);
      outline: none;
      padding: 0 12px;
      color: rgba(255, 255, 255, 0.9);
      transition: 0.2s;
    }

    .btn-register {
      position: absolute;
      // 下划线
      text-decoration: underline;
      color: rgb(149, 200, 218);
      font-size: 16px;
      margin-left: 237px;
      // margin-top: 6px;
    }
  }
}

ul li {
  position: absolute;
  border: 1px solid #fff;
  background-color: #fff;
  width: 30px;
  height: 30px;
  list-style: none;
  opacity: 0;
}
.square li {
  top: 40vh;
  left: 60vw;
  /* 执行动画：动画名 时长 线性的 无限次播放 */
  animation: square 10s linear infinite;
}
.square li:nth-child(2) {
  top: 80vh;
  left: 10vw;
  /* 设置动画延迟时间 */
  animation-delay: 2s;
}
.square li:nth-child(3) {
  top: 80vh;
  left: 85vw;
  /* 设置动画延迟时间 */
  animation-delay: 4s;
}
.square li:nth-child(4) {
  top: 10vh;
  left: 70vw;
  /* 设置动画延迟时间 */
  animation-delay: 6s;
}
.square li:nth-child(5) {
  top: 10vh;
  left: 10vw;
  /* 设置动画延迟时间 */
  animation-delay: 8s;
}
.circle li {
  bottom: 0;
  left: 15vw;
  /* 执行动画 */
  animation: circle 10s linear infinite;
}
.circle li:nth-child(2) {
  left: 35vw;
  /* 设置动画延迟时间 */
  animation-delay: 2s;
}
.circle li:nth-child(3) {
  left: 55vw;
  /* 设置动画延迟时间 */
  animation-delay: 6s;
}
.circle li:nth-child(4) {
  left: 75vw;
  /* 设置动画延迟时间 */
  animation-delay: 4s;
}
.circle li:nth-child(5) {
  left: 90vw;
  /* 设置动画延迟时间 */
  animation-delay: 8s;
}

/* 定义动画 */
@keyframes square {
  0% {
    transform: scale(0) rotateY(0deg);
    opacity: 1;
  }
  100% {
    transform: scale(5) rotateY(1000deg);
    opacity: 0;
  }
}
@keyframes circle {
  0% {
    transform: scale(0) rotateY(0deg);
    opacity: 1;
    bottom: 0;
    border-radius: 0;
  }
  100% {
    transform: scale(5) rotateY(1000deg);
    opacity: 0;
    bottom: 90vh;
    border-radius: 50%;
  }
}
.a {
  box-shadow: 2px 6px 8px 8px rgba(27, 26, 26, 0.3);
  border-radius: 15px;
  position: relative;
  top: 0px;
  width: 1000px;
  height: 600px;
  background-image: url("@/assets/images/login_bg2.0.jpg");
  background-size: cover;
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
}
.b,
.c,
.d,
.e {
  position: absolute;
  width: 1000px;
  height: 600px;
  /* 设置阴影 */
  filter: drop-shadow(4px 4px 12px rgba(0, 0, 0, 0.5));
  background-size: cover;
  opacity: 0.7;
  transition: 0.5s;
}
.b::after,
.c::after,
.d::after,
.e::after {
  content: "";
  position: absolute;
  width: 1000px;
  height: 600px;
  background-image: url("@/assets/images/login_bg2.0.jpg");
}
.b {
  left: -400px;
  transform: rotateZ(100deg);
  overflow: hidden;
}
.b::after {
  transform: rotateZ(-100deg);
}

.c {
  left: -400px;
  transform: rotateZ(-100deg);
  overflow: hidden;
}
.c::after {
  transform: rotateZ(100deg);
}

.d {
  right: -400px;
  transform: rotateZ(105deg);
  overflow: hidden;
}
.d::after {
  transform: rotateZ(-105deg);
}

.e {
  right: -400px;
  transform: rotateZ(-100deg);
  overflow: hidden;
}
.e::after {
  transform: rotateZ(100deg);
}
.f {
  z-index: 99;
  opacity: 0;
  font: 900 50px "";
  /* 设置字体间距 */
  letter-spacing: 10px;
  color: rgb(60, 60, 70);
  transition: 0.5s;
}
/* 设置动画 */
.a:hover .b {
  left: -550px;
}
.a:hover .c {
  left: -600px;
}
.a:hover .d {
  right: -550px;
}
.a:hover .e {
  right: -610px;
}
.a:hover .f {
  opacity: 1;
}
</style>