<template>
  <el-card class="box-card">
    <div slot="header" class="clearfix">
      <span style="font-weight: 900; font-size: large">基本资料</span>
    </div>
    <!-- 表单 -->
    <el-form
      :model="userForm"
      :rules="userRules"
      ref="userFormRef"
      label-width="100px"
      class="demo-ruleForm"
    >
      <el-form-item label=" 登录名称： " prop="Username">
        <el-input v-model="userForm.Username" disabled></el-input>
      </el-form-item>
      <el-form-item label=" 用户昵称： " prop="Nickname">
        <el-input v-model="userForm.Nickname"></el-input>
      </el-form-item>
      <el-form-item label=" 用户邮箱： " prop="Email">
        <el-input v-model="userForm.Email"></el-input>
      </el-form-item>

      <el-form-item label=" 用户头像： " prop="Avatar">
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
      </el-form-item>

      <el-form-item>
        <el-button type="primary" @click="submit">立即创建</el-button>
        <el-button @click="resetForm" style="margin-left: 6%">重置</el-button>
      </el-form-item>
    </el-form>
  </el-card>
</template>

<script>
import { updateUserInfo } from "@/api/userApi";
import { getToken } from "@/utils/auth";
import baseUrl from "@/config/appConfig";
// import { getuid } from "process";
// import store from '@/store';
export default {
  name: "UserInfo",
  inject: ["reload"],
  data() {
    return {
      actionHeader: { Authorization: "Bearer " + getToken() },
      actionUrl: baseUrl.baseUrl + "/files",
      userForm: { ...this.$store.state.userInfo },
      avatar: "",
      userRules: {
        Username: [],
        Nickname: [
          { required: true, message: "请输入用户昵称", trigger: "blur" },
          {
            pattern: /^\S{1,10}$/,
            message: "用户昵称长度在 1到 10 个字符",
            trigger: "blur",
          },
        ],
        Email: [
          { required: true, message: "请输入邮箱", trigger: "blur" },
          { type: "email", message: "请输入正确的邮箱格式", trigger: "blur" },
        ],
      },
      imageUrl: "",
    };
  },
  created() {
    console.log(this.$store.state.userInfo.Avatar);
    var TheImageUrl = this.$store.state.userInfo.Avatar;
    this.imageUrl = `${baseUrl.baseUrl}/files/${TheImageUrl}`;
  },
  methods: {
    handleAvatarSuccess(res, file) {
      this.avatar = res.data[0];
      this.$store.state.userInfo.Avatar = res.data[0];
      this.imageUrl = `${baseUrl.baseUrl}/files/${res.data[0]}`;
    },

    beforeAvatarUpload(file) {
      const isJPG = file.type === "image/jpeg";
      const isLt2M = file.size / 1024 / 1024 < 5;

      if (!isJPG) {
        this.$message.error("上传头像图片只能是 JPG 格式!");
      }
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 5MB!");
      }
      return isJPG && isLt2M;
    },
    resetForm() {
      this.userForm = { ...this.$store.state.userInfo };
    },
    submit() {
      this.$confirm("确定修改？", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "info",
      })
        .then(() => {
          this.$refs.userFormRef.validate(async (valid) => {
            if (valid) {
              var data = {
                Username: this.userForm.Username,
                Password: this.userForm.Password,
                Nickname: this.userForm.Nickname,
                Avatar: this.$store.state.userInfo.Avatar,
                Email: this.userForm.Email,
              };
              console.log(data);
              updateUserInfo(this.userForm.Id, data).then((res) => {
                // console.log(res);
                setTimeout(() => {
                  this.$store.commit("updataUserInfo", res.data);
                  this.reload();
                }, 500);
              });
              this.userForm = { ...this.$store.state.userInfo };
            }
          });
          this.$message({
            type: "success",
            message: "修改成功!",
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消修改",
          });
        });
      // updateUserInfo().then(res=>{
      //   console.log(res);
      // })
      // this.$refs.userFormRef.validate(async (valid) => {
      //   if (valid) {
      //     var data = {
      //       Username: this.userForm.Username,
      //       Password: this.userForm.Password,
      //       Nickname: this.userForm.Nickname,
      //       Avatar: this.avatar,
      //       // email: this.userForm.email
      //     };
      //     updateUserInfo(this.userForm.Id, data).then((res) => {
      //       // console.log(res);
      //       this.$store.commit("updataUserInfo", res.data);
      //     });
      //   }
      // });
    },
  },
};
</script>

<style lang="less" scoped>
.el-form {
  width: 500px;
  margin-left: 28%;
  padding-right: 2%;
  padding-top: 2%;
  padding-bottom: 2%;
  border-radius: 12px;
  border-right: 3.5px double rgb(244, 197, 197);
  border-top: 3.5px double rgb(128, 221, 243);
  border-bottom: 3.5px double rgb(241, 232, 163);
  border-left: 3.5px double rgb(215, 180, 241);
}
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
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
</style>