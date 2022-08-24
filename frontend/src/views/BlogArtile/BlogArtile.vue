<template>
  <div>
    <div>
      <menu-component @func="getKeyword"></menu-component>
    </div>

    <div class="app">
      <div style="position: fixed">
        <!-- <video
          id="v1"
          autoplay
          muted
          loop
          style="width: 1520px; height: 100% !important"
          playbackRate="0.5"
        >
          <source src="../../assets/videoes/云上夕阳.mp4" />
        </video> -->
      </div>
      <!-- 博客详情页总盒子 -->
      <div class="blog-big-box">
        <!-- 博客详情页头 -->
        <div class="blog-header">
          <!-- 页头封面 -->
          <div class="header-photo">
            <el-image
              :src="src"
              style="height: 100%; width: 100%; border-radius: 5px"
            ></el-image>
          </div>
        </div>

        <!-- 博客数据 -->
        <div
          class="blog"
          v-for="art in blogarticle"
          :key="art.appUserId"
          style="position: relative"
        >
          <!-- 博主数据 -->
          <!-- 博客数据、目录...父级盒子 -->
          <div
            class="blog-data-box"
            v-for="blo in blogger"
            :key="blo.appUserId"
          >
            <!-- 博客数据 -->
            <el-card class="blog-data-card">
              <!-- 头像父级盒子 -->
              <div class="avatar">
                <el-avatar
                  style="width: 75px; height: 75px"
                  :src="art.avatar"
                  class="avatar-photo"
                ></el-avatar>
                <!-- src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png" -->
              </div>
              <el-card class="blogger-information">
                <div class="blogger-inf">
                  <h3 class="blogger-name">博主：</h3>
                  <h4 class="blogger-nickname">{{ art.nickname }}</h4>
                </div>
              </el-card>
              <!-- 推荐指数 -->
              <div class="recommendation">
                <el-rate
                  v-model="recommendationvalue"
                  disabled
                  show-score
                  text-color="#ff9900"
                  score-template="{value}"
                >
                </el-rate>
              </div>

              <el-divider></el-divider>

              <!-- 博客流量信息栏 -->
              <div class="blog-stream">
                <span class="fans">粉丝</span>
                <el-divider direction="vertical"></el-divider>
                <span class="praise">获赞</span>
                <el-divider direction="vertical"></el-divider>
                <span class="commentnum">评论</span>
                <el-divider direction="vertical"></el-divider>
                <span class="browernum">浏览</span>
              </div>

              <el-divider></el-divider>
              <el-divider></el-divider>

              <!-- 博客流量数量 -->
              <div class="tag-group">
                <el-tag type=" " effect="plain" class="fansfans">
                  <!-- <span>粉丝:</span> -->
                  {{ blo.fans }}</el-tag
                >
                <el-tag type="danger" effect="plain" class="praisepraise">
                  <!-- <span>获赞:</span> -->
                  {{ art.articlePraise }}</el-tag
                >
                <el-tag
                  type="success"
                  effect="plain"
                  class="commentnumcommentnum"
                >
                  <!-- <span>评论:</span> -->
                  {{ art.commentsNumber }}</el-tag
                >
                <el-tag
                  type="warning"
                  effect="plain"
                  class="browernumbrowernum"
                >
                  <!-- <span>浏览:</span> -->
                  {{ art.viewstsNumber }}</el-tag
                >
              </div>

              <!-- </br> -->

              <el-divider></el-divider>

              <!-- 博客流量信息栏2 -->
              <div class="blog-stream-two">
                <span class="collect">收藏</span>
                <el-divider direction="vertical"></el-divider>
                <span class="visit">访问</span>
                <el-divider direction="vertical"></el-divider>
                <span class="ranking">排名</span>
                <el-divider direction="vertical"></el-divider>
                <span class="original">原创</span>
              </div>

              <el-divider></el-divider>
              <el-divider></el-divider>
              <el-divider></el-divider>

              <!-- 博客流量数量2 -->
              <div class="tag-group-two">
                <el-tag type=" " effect="plain" class="collectcollect">
                  <!-- <span>收藏:</span> -->
                  0</el-tag
                >
                <el-tag type="danger" effect="plain" class="visitvisit">
                  <!-- <span>访问:</span> -->
                  0</el-tag
                >
                <el-tag type="success" effect="plain" class="rankingranking">
                  <!-- <span>排名:</span> -->
                  0</el-tag
                >
                <el-tag type="warning" effect="plain" class="originaloriginal">
                  <!-- <span>原创:</span> -->
                  {{ blo.original }}</el-tag
                >
              </div>
            </el-card>

            <!-- 名言框 -->
            <el-card
              class="blog-wisdom-card"
              v-for="wis in wisdom"
              :key="wis.code"
            >
              <h2 class="wisdom-title">✨ 每日名言 ：</h2>
              <el-card style="font-size: larger">
                {{ wis.witticism }}
              </el-card>
            </el-card>

            <!-- 推荐区 -->
            <el-card class="blog-recommend">
              <el-divider></el-divider>
              <el-divider></el-divider>

              <!-- 博客推荐 -->
              <el-card
                v-for="rec in recommend"
                :key="rec.code"
                class="recommend-card"
              >
                <div class="recommend">
                  <a :href="rec.url" target="_blank">{{ rec.title }}</a>
                </div>
              </el-card>

              <el-divider></el-divider>
              <el-divider></el-divider>
              <el-divider></el-divider>
            </el-card>
          </div>

          <div class="title-right">
            <span class="title-right-text"
              >software technology 2 0 软一一组</span
            >
          </div>

          <!-- 博客详情父级盒子 -->
          <div class="blog-box">
            <!-- 博客详情 -->
            <el-card class="blog-card">
              <!-- 博客标签 -->
              <div class="blog-tag">
                <el-tag class="tag1">标签一</el-tag>
                <el-tag type="success" class="tag2">标签二</el-tag>
                <el-tag type="info" class="tag3">标签三</el-tag>
                <el-tag type="warning" class="tag4">标签四</el-tag>
                <el-tag type="danger" class="tag5">标签五</el-tag>
              </div>
              <div slot="header" class="clearfix">
                <!-- 博客标题 -->
                <div class="title">
                  <h2>{{ art.articleTitle }}</h2>
                  <el-divider></el-divider>
                  <el-divider></el-divider>

                  <div class="column">
                    <el-tag>所属栏目专区 >></el-tag>
                    <el-tag class="appArticleClassName">{{
                      art.appArticleClassName
                    }}</el-tag>
                  </div>
                  <el-divider></el-divider>
                  <el-divider></el-divider>
                </div>
              </div>

              <!-- 博客文章内容 -->
              <div class="blog-content">
                <el-card class="content-card">
                  <el-divider></el-divider>
                  <el-divider></el-divider>
                  <div class="content">
                    <div v-html="art.articleContent"></div>
                  </div>
                  <el-divider></el-divider>
                  <el-divider></el-divider>
                </el-card>
              </div>

              <!-- 用户操作框 -->
              <div class="useroperation">
                <!-- 用户评论框 -->
                <el-input
                  placeholder="留下你的评论叭~"
                  v-model="comments"
                  clearable
                  style="width: 400px"
                  class="usercomment"
                >
                </el-input>

                <!-- 用户评论发送点击按钮 -->
                <el-button
                  icon="el-icon-s-promotion"
                  class="comment-btn"
                  type="primary"
                  @click="addComment"
                  >发布评论</el-button
                >

                <!-- 用户操作 -->
                <div class="operation">
                  <!-- 点赞 -->
                  <el-row>
                    <el-button
                      class="praise-btn"
                      @click="giveALike"
                      type="primary"
                      icon="el-icon-thumb"
                      circle
                    ></el-button>
                    <!-- 收藏 -->
                    <el-button
                      disabled
                      class="collect-btn"
                      type="success"
                      icon="el-icon-star-off"
                      circle
                    ></el-button>
                    <!-- 转发 -->
                    <el-button
                      disabled
                      class="transmit-btn"
                      type="info"
                      icon="el-icon-position"
                      circle
                    ></el-button>
                    <!-- 举报 -->
                    <el-button
                      disabled
                      class="report-btn"
                      type="warning"
                      icon="el-icon-warning-outline"
                      circle
                    ></el-button>
                  </el-row>
                </div>
              </div>
            </el-card>

            <!-- 评论区 -->
            <el-card class="blog-comment-card">
              <el-divider></el-divider>
              <el-divider></el-divider>
              <!-- 用户评论区 -->
              <el-card
                class="user-comment-card"
                v-for="com in usercomment"
                :key="com.Id"
              >
                <div class="user-comment">
                  {{ com.nickname }}：{{ com.commentContent }}
                </div>
              </el-card>
              <el-divider></el-divider>
              <el-divider></el-divider>
              <el-divider></el-divider>
            </el-card>
          </div>
        </div>
      </div>
      <!-- 到底了提示父级盒子 -->
      <div class="bottomtipbox">
        <el-divider></el-divider>
        <el-divider></el-divider>

        <div class="bottom-tip">
          <span class="bottomtext"
            >到底啦 ~ 没有啦 ~ 康康其他内容叭 ~ _(:з)∠)_</span
          >
        </div>
      </div>
    </div>
  </div>
</template>


<style src="../../style/blog/blog.css" scoped>
</style>

<script>
import MenuComponent from "@/components/menu.vue";
import dayjs from "dayjs";
import baseUrl from "@/config/appConfig";
import { getArticlesId, updateArticleInfo } from "@/api/articleApi";
import {
  getArticleCommentById,
  addArticleComment,
} from "@/api/articleCommentApi";
export default {
  inject: ["reload"],
  components: {
    MenuComponent,
  },
  data() {
    return {
      wisdom: witticismdata, // 名言
      recommend: recommenddata, // 推荐博客链接
      src: headerphoto, // 页头封面
      input: "", // 搜索框
      isGiveALike: "", // 点赞
      blogger: bloggerdata, // 博主数据
      blogarticle: blogarticledata, // 文章数据
      usercomment: usercommentdata, // 用户评论
      recommendationvalue: 4.2, // 推荐指数
      scorevalue: 5, //博客评分
      comments: "", // 评论内容
      iconClasses: ["icon-rate-face-1", "icon-rate-face-2", "icon-rate-face-3"], // 博客评分表情，等同于 { 2: 'icon-rate-face-1', 4: { value: 'icon-rate-face-2', excluded: true }, 5: 'icon-rate-face-3' }
    };
  },
  methods: {
    giveALike() {
      var userInfo = this.$store.state.userInfo;
      var GiveArray = this.$store.state.isGaveALike;
      this.$store.commit("updateisGaveALike", GiveArray);
      if (GiveArray.indexOf(userInfo.Username) != -1) {
        this.isGiveALike = false;
      } else {
        this.isGiveALike = true;
      }
      if (this.isGiveALike == true) {
        this.blogarticle[0].articlePraise++;
        // console.log(this.blogarticle[0].articlePraise);
        updateArticleInfo(this.blogarticle[0].id, this.blogarticle[0]);
        GiveArray.push(userInfo.Username);
        this.$store.commit("updateisGaveALike", GiveArray);
      } else {
        this.blogarticle[0].articlePraise--;
        // console.log(this.blogarticle[0].articlePraise);
        updateArticleInfo(this.blogarticle[0].id, this.blogarticle[0]);
        GiveArray.splice(userInfo.Username, 1);
        this.$store.commit("updateisGaveALike", GiveArray);
      }
    },
    getKeyword() {
      this.$router.push("/cmsmain");
    },
    addComment() {
      var UserCommentInfo = {
        UserCommentId: this.$store.state.userInfo.Id,
        CommentContent: this.comments,
        AppUserId: this.blogarticle[0].appUserId,
        AppArticleId: this.blogarticle[0].id,
      };
      // console.log(UserCommentInfo);
      addArticleComment(UserCommentInfo).then((res) => {
        if (res.code == 40001) {
          this.$message.success("评论成功");
          this.reload();
        } else {
          this.$message.warning("评论失败");
        }
      });
    },
  },
  created() {
    var id = this.$route.query.id;
    getArticlesId(id).then((res) => {
      this.blogarticle = res;
      this.blogarticle[0].avatar = baseUrl.baseUrl + "/files/" + res[0].avatar;
      this.blogarticle[0].createdAt = dayjs(res[0].createdAt).format(
        "YYYY-MM-DD"
      );
      this.blogarticle[0].viewstsNumber++;
      // console.log(this.blogarticle[0].articleContent);
      // this.blogarticle[0].articleContent =
      //   this.blogarticle[0].articleContent.split("img")[0] +
      //   `img style="width:400px;height:300px"` +
      //   this.blogarticle[0].articleContent.split("img")[1];
      // console.log(this.blogarticle[0].articleContent);
      // console.log(this.blogarticle[0]);

      // updateArticleInfo(this.blogarticle[0].id, this.blogarticle[0]);
    });
    getArticleCommentById(id).then((res) => {
      // console.log(res);
      this.blogarticle[0].commentsNumber = res.length;
      this.usercomment = res;
      this.usercomment.forEach((item) => {
        item.avatar = baseUrl.baseUrl + "/files/" + item.avatar;
        item.createdAt = dayjs(item.createdAt).format(" YYYY-MM-DD");
      });
    });
  },
};
// 数据存储区

// 名言
let witticismdata = [
  {
    code: 0,
    witticism:
      "“我有个朋友，每次见到我都说要建个网站。三年过去，什么动静都没有。以后他再和我说同样的话题，我就毫不客气，立刻打断他。”——阿里巴巴马云",
  },
];

// 博客推荐
let recommenddata = [
  {
    code: 0,
    url: "https://www.cnblogs.com/lulianqi/p/16476166.html",
    title: "HTTPS 如何保证数据传输安全",
  },
  {
    code: 1,
    url: "https://www.cnblogs.com/coco1s/p/16472985.html",
    title: "利用噪声构建美妙的 CSS 图形",
  },
  {
    code: 2,
    url: "https://www.cnblogs.com/sealio/p/16497757.html",
    title: "21条最佳实践，全面保障 GitHub 使用安全",
  },
  {
    code: 3,
    url: "https://www.cnblogs.com/shengshengwang/p/16500644.html",
    title: "基于ABP实现DDD--领域服务、应用服务和DTO实践",
  },
];

// 用户评论
let usercommentdata = [
  {
    code: 0,
    username: "倔强的青铜",
    comment: "666",
  },
  {
    code: 1,
    username: "路过的过桥米线",
    comment: "哇哦，涨知识了！",
  },
];

// 博客数据
let blogarticledata = [
  {
    code: 0,
    praise: 9999,
    commentnum: 9999,
    brower: 9999,
    collect: 7777,
    visit: 6666,
    ranking: 2000,
    title: "你好！这里是博客文章标题",
    content: "你好！这里是博客文章详情内容",
  },
];

// 博主数据
let bloggerdata = [
  {
    code: 0,
    nickname: "太阳下山有月光",
    avatar:
      "https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png",
    fans: 8888,
    original: 1000,
  },
];

// 页头封面数据
let headerphoto = require("@/assets/images/公司长横图logo.jpg");
</script>

