<template>
  <div class="personalCenter">
    <el-container>
      <el-header>
        <menu-component></menu-component>
      </el-header>
      <el-container>
        <el-aside width="400px">
          <el-col :span="12">
            <div class="sub-title"></div>
            <div class="demo-basic--circle" :data="userInfo">
              <div class="img">
                <el-avatar
                  :size="100"
                  :src="userInfo.Avatar"
                  align="center"
                  style="
                    border: 3.5px double;
                    background-color: rgba(244, 225, 225, 0.1);
                  "
                ></el-avatar>
              </div>
              <div class="block">
                <span>{{ userInfo.Username }}</span>
              </div>
            </div>
          </el-col>
          <el-input
            class="inputbtn"
            v-model="userInfo.Username"
            type="text"
            placeholder="用户ID"
            maxlength="6"
            show-word-limit
          ></el-input>
          <el-input
            class="inputbtn"
            v-model="userInfo.Nickname"
            type="text"
            placeholder="用户昵称"
            show-word-limit
          ></el-input>
          <el-input
            class="inputbtn"
            v-model="userInfo.Password"
            placeholder="请输入密码"
            show-password
          ></el-input>
          <el-input
            class="inputbtn"
            v-model="input6"
            type="text"
            placeholder="用户权限"
            maxlength="2"
            show-word-limit
          ></el-input>
          <el-input
            class="inputbtn"
            v-model="userInfo.Email"
            placeholder="邮箱"
          >
          </el-input>
          <el-input
            class="inputbtn"
            v-model="input8"
            type="text"
            placeholder="手机号码"
            maxlength="20"
            show-word-limit
          ></el-input>
          <el-button-group style="float: right; padding: 3px 0" type="text">
            <el-button
              type="primary"
              size="medium"
              round
              @click="updataUser"
              class="save"
              >保存</el-button
            >
          </el-button-group>
        </el-aside>
        <el-container>
          <el-main>
            <el-card class="box-card">
              <div slot="header" class="clearfix">
                <span style="float: left; font-weight: 900; font-size: large"
                  ><b>账号绑定</b></span
                >
                <el-button-group
                  style="float: right; padding: 3px 0"
                  type="text"
                >
                  <el-button
                    type="primary"
                    icon="el-icon-edit"
                    size="medium"
                    style="border: 3.5px double white"
                    round
                  >
                    修改信息
                  </el-button>
                  <el-button
                    type="primary"
                    icon="el-icon-check"
                    size="medium"
                    style="border: 3.5px double white"
                    round
                  >
                    确认修改
                  </el-button>
                </el-button-group>
              </div>
              <div v-for="o in 1" :key="o" class="text item">
                <el-table
                  :data="tableData"
                  style="
                    width: 100%;
                    border-radius: 14px;
                    border: 4.5px double pink;
                  "
                >
                  <el-table-column label="账号名" width="180">
                    <template slot-scope="scope">
                      <p>{{ scope.row.Username }}</p>
                      <div slot="reference" class="name-wrapper"></div>
                    </template>
                  </el-table-column>
                  <el-table-column label="操作" align="center">
                    <template slot-scope="scope">
                      <el-button
                        size="mini"
                        @click="handleEdit(scope.$index, scope.row)"
                      >
                        编辑
                      </el-button>
                      <el-button
                        size="mini"
                        type="danger"
                        @click="handleDelete(scope.$index, scope.row)"
                      >
                        解绑
                      </el-button>
                    </template>
                  </el-table-column>
                  <el-table-column label="更多" align="center">
                    <el-row>
                      <el-button
                        type="info"
                        icon="el-icon-message"
                        circle
                      ></el-button>
                      <el-button
                        type="warning"
                        icon="el-icon-star-off"
                        circle
                      ></el-button>
                      <el-button
                        type="share"
                        icon="el-icon-share"
                        circle
                      ></el-button>
                    </el-row>
                  </el-table-column>
                </el-table>
              </div>
              <div v-for="o in 1" :key="o" class="text item">
                <el-table
                  :data="tableData"
                  style="
                    width: 100%;
                    border-radius: 14px;
                    border: 4.5px double cornflowerblue;
                  "
                >
                  <el-table-column label="账号名" width="180">
                    <template slot-scope="scope">
                      <p>{{ scope.row.name2 }}</p>
                      <div slot="reference" class="name-wrapper"></div>
                    </template>
                  </el-table-column>
                  <el-table-column label="操作" align="center">
                    <template slot-scope="scope">
                      <el-button
                        size="mini"
                        @click="handleEdit(scope.$index, scope.row)"
                      >
                        编辑
                      </el-button>
                      <el-button
                        size="mini"
                        type="danger"
                        @click="handleDelete(scope.$index, scope.row)"
                      >
                        解绑
                      </el-button>
                    </template>
                  </el-table-column>
                  <el-table-column label="更多" align="center">
                    <el-row>
                      <el-button
                        type="info"
                        icon="el-icon-message"
                        circle
                      ></el-button>
                      <el-button
                        type="warning"
                        icon="el-icon-star-off"
                        circle
                      ></el-button>
                      <el-button
                        type="share"
                        icon="el-icon-share"
                        circle
                      ></el-button>
                    </el-row>
                  </el-table-column>
                </el-table>
              </div>
            </el-card>
            <el-card class="box-card">
              <div>
                <span
                  style="float: left; font-weight: 900; font-size: large"
                  shadow="hover"
                  ><b>个人说明</b></span
                >
                <br />
                <br />
                <span style="font-weight: 900; font-size: 15px"
                  >螃蟹在剥我的壳</span
                >
                <el-divider></el-divider>
                <span style="font-weight: 900; font-size: 15px"
                  >笔记本在写我</span
                >
                <el-divider></el-divider>
                <span style="font-weight: 900; font-size: 15px"
                  >漫天的我落在枫叶的雪花上</span
                >
              </div>
            </el-card>
          </el-main>
        </el-container>
      </el-container>
    </el-container>
  </div>
</template>
<script>
import MenuComponent from "@/components/menu.vue";
import { getUserInfo, updateUserInfo } from "@/api/userApi";
import baseUrl from "@/config/appConfig"
import dayjs from "dayjs";
export default {
  inject: ["reload"],
  name: "PersonalCenter",
  components: {
    MenuComponent,
  },
  data() {
    return {
      avatarUrl: "",
      userInfo: "",
      input6: "",
      input8: "",
      text: "",
      sizeList: ["large"],
      tableData: [
        {
          date: "2016-05-03",
          name1: "Wechat",
          name2: "Github",
        },
      ],
    };
  },
  created() {
    var username = this.$store.state.userInfo.Username;
    getUserInfo(username).then((res) => {
      this.userInfo = res.data;
      this.avatarUrl = res.data.Avatar;
      this.userInfo.Avatar =
        `${baseUrl.baseUrl}/files/` + this.userInfo.Avatar;
      this.userInfo.CreatedAt = dayjs(this.userInfo.CreatedAt).format(
        " YYYY-MM-DD"
      );
      console.log(this.userInfo);
    });
  },
  methods: {
    handleEdit(index, row) {
      console.log(index, row);
    },
    handleDelete(index, row) {
      console.log(index, row);
    },
    updataUser() {
      var id = this.$store.state.userInfo.Id;
      var userInfo = {
        Avatar: this.avatarUrl,
        Nickname: this.userInfo.Nickname,
        Username: this.userInfo.Username,
        Password: this.userInfo.Password,
        Email: this.userInfo.Email,
      };
      console.log(userInfo);
      this.$confirm("确认修改?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          updateUserInfo(id, userInfo).then((res) => {
            if (res.code == 10002) {
              this.$message.success("修改成功");
              this.reload();
            } else {
              this.$message.info("修改失败");
            }
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消修改",
          });
        });
    },
  },
};
</script>
<style lang="less" scoped>
.el-header {
  line-height: 60px;
  text-align: center;
  background-color: #ffffff;
}
.el-aside {
  line-height: 44px;
  text-align: center;
  // background-color: #a6b4c5;
  // background-color: rgba(244, 225, 225, 0.7);
  // background: url(5.gif);
  // border-top: 3.5px double rgb(97, 208, 233);
  // border-bottom: 3.5px double rgb(239, 188, 188);
  border-right: 3.5px double rgb(198, 164, 223);
  // backdrop-filter: blur(10px);
}
.el-main {
  line-height: 36px;
  text-align: center;
  // background-color: #e9eef3;
  // background: url(5.gif);
}

.demo-basic--circle {
  margin-top: 30px;
  margin-left: 150px;
}
.block {
  margin-left: 25px;
  font-weight: bold;
}
.text {
  font-size: 14px;
}
.item {
  margin-bottom: 18px;
}
.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}

.box-card {
  margin-bottom: 2%;
  // width: 1368px;
  width: 1076px;
  border-radius: 30px;
  // border-radius: 10px;
  border-top: 3.5px double rgb(97, 208, 233);
  border-bottom: 3.5px double rgb(239, 188, 188);
  border-left: 3.5px double rgb(198, 164, 223);
  border-right: 3.5px double rgb(236, 228, 114);
  background-color: rgba(218, 208, 208, 0.4);
  // backdrop-filter: blur(10px);
}

.inputbtn {
  margin-left: 2%;
  margin-right: 2%;
  border-radius: 6px;
  width: 320px;
  margin-bottom: 3%;
  border-bottom: 3.5px double rgb(239, 188, 188);
  border-left: 3.5px double rgb(198, 164, 223);
  border-right: 3.5px double rgb(236, 228, 114);
}

.save {
  width: 100px;
  margin-top: 5%;
  margin-left: -40%;
  border: 4px double rgb(244, 237, 237);
  font-size: 17px;
  font-weight: 500;
}
</style>

