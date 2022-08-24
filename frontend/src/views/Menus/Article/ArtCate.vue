<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix header-box">
        <span style="font-weight: 900; font-size: large">文章分类</span>
        <el-button
          type="primary"
          size="mini"
          @click="addlogVisible = !addlogVisible"
          >添加分类</el-button
        >
      </div>
      <el-table :data="ArtCateList" style="width: 100%" border>
        <el-table-column type="index" label="序号" width="50">
        </el-table-column>
        <el-table-column prop="AppArticleClassName" label="名称">
        </el-table-column>
        <el-table-column prop="CreatedAt" label="日期"> </el-table-column>
        <el-table-column label="操作">
          <template v-slot="{ row }">
            <el-button type="primary" @click="ModifyArt(row)">修改</el-button>
            <el-button type="danger" @click="deleteArt(row.Id)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
    <!--修改文章分类 -->
    <el-dialog
      title="修改文章分类"
      :visible.sync="ModifylogVisible"
      width="35%"
      @closed="$refs.ModifyFormRef.resetFields()"
    >
      <el-form
        ref="ModifyFormRef"
        :model="ModifyForm"
        :rules="addFormRules"
        label-width="80px"
      >
        <el-form-item label="分类名称" prop="AppArticleClassName">
          <el-input v-model="ModifyForm.AppArticleClassName"></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="ModifylogVisible = false">取 消</el-button>
        <el-button type="primary" @click="ModifyArtCate">确 定</el-button>
      </span>
    </el-dialog>
    <!-- 添加文章分类 -->
    <el-dialog
      title="添加文章分类"
      :visible.sync="addlogVisible"
      width="35%"
      @closed="$refs.addFormRef.resetFields()"
    >
      <el-form
        ref="addFormRef"
        :model="addForm"
        :rules="addFormRules"
        label-width="80px"
      >
        <el-form-item label="分类名称" prop="AppArticleClassName">
          <el-input v-model="addForm.AppArticleClassName"></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="addlogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addArtCate">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import {
  getArticleClass,
  deleteArticleClass,
  addArticleClass,
  updateArticleClassInfo,
} from "@/api/articleClassApi";
import dayjs from "dayjs";
export default {
  name: "ArtCate",
  inject: ["reload"],
  data() {
    return {
      ArtCateList: [],
      addlogVisible: false,
      ModifylogVisible: false,
      addForm: {
        AppArticleClassName: "",
      },
      ModifyForm: {},
      addFormRules: {
        AppArticleClassName: [
          { required: true, message: "请输入分类名称", trigger: "blur" },
          {
            pattern: /^\S{1,10}$/,
            message: "分类名必须是1-10位的非空字符",
            trigger: "blur",
          },
        ],
      },
    };
  },
  created() {
    //获取分类列表数据
    getArticleClass().then((res) => {
      console.log(res);
      this.ArtCateList = res.data;
      this.ArtCateList.forEach((item) => {
        item.CreatedAt = dayjs(item.CreatedAt).format(" YYYY-MM-DDHH:mm:ss");
      });
      // console.log(this.ArtCateList);
    });
  },
  methods: {
    // 添加文章分类
    addArtCate() {
      this.$refs.addFormRef.validate(async (valid) => {
        if (valid) {
          addArticleClass(this.addForm).then((res) => {
            if (res.code == 30001) {
              this.$message.success("添加成功");
              this.reload();
            }
          });
        }
        this.addlogVisible = false;
      });
    },
    deleteArt(id) {
      this.$confirm("此操作将永久删除该文件, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          deleteArticleClass(id).then((res) => {
            // console.log(res);
            if (res.code == 30003) {
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
    ModifyArt(obj) {
      // console.log(obj.AppArticleClassName);
      this.$nextTick(() => {
        this.ModifylogVisible = true;
        this.ModifyForm = obj;
      });
    },
    // 修改文章分类
    ModifyArtCate() {
      this.ModifylogVisible = false;
      this.$refs.ModifyFormRef.validate((valid) => {
        if (valid) {
          var data = {
            AppArticleClassName: this.ModifyForm.AppArticleClassName,
          };
          updateArticleClassInfo(this.ModifyForm.Id, data).then((res) => {
            if (res.code == 30002) {
              this.$message.success("修改成功");
              this.reload();
            } else {
              this.$message.warning("修改失败");
            }
          });
        }
        this.ModifylogVisible = false;
      });
    },
  },
};
</script>

<style lang="less" scoped>
.header-box {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
