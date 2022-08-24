<template>
  <div class="app">
    <menu-component @func="getKeyword"></menu-component>
    <!-- <div style="position: fixed">
      <video
        id="v1"
        autoplay
        muted
        loop
        style="width: 1520px; height: 100% !important"
        playbackRate="0.5"
      >
        <source src="../../assets/videoes/云上夕阳.mp4" />
      </video>
    </div> -->
    <div class="mainbottom">
      <!-- 头部轮播图父级盒子 -->
      <div class="fatherbox">
        <!-- 头部轮播图 -->
        <div class="headbox">
          <el-carousel :interval="3500" type="card" height="225px">
            <el-carousel-item v-for="item in photos" :key="item.code">
              <img :src="item.codeview" style="width: 100%" />
            </el-carousel-item>
          </el-carousel>
        </div>

        <!-- 尾部分类栏文章内容热度父级盒子 -->
        <div class="articlehostbox">
          <!-- 分类栏 -->
          <div class="columnbox">
            <!-- 左边大卡片 -->
            <el-card class="column-card">
              <!-- 分类栏头部 -->
              <div class="column-header">
                <!-- 日历 -->
                <el-calendar v-model="timevalue"> </el-calendar>
              </div>
              <span style="font-size: larger; color: pink; margin-left: 2%"
                >.:*☆☆*:.｡.｡:*☆.☆*:｡.｡.:*☆☆*:....</span
              >
              <!-- 分类栏标题 -->
              <div>
                <el-divider
                  ><span style="font-size: 23px; color: coral"
                    >栏目分类</span
                  ></el-divider
                >
              </div>

              <!-- 分类栏尾部 -->
              <!-- <div class="column-bottom"> -->
              <!-- <el-cascader :props="props"></el-cascader> -->
              <el-radio-group v-model="direction"> </el-radio-group>

              <el-button
                @click="drawer = true"
                type="primary"
                style="margin-left: 11%"
                icon="el-icon-s-grid"
              >
                点我打开博客分类栏 >
              </el-button>

              <el-divider></el-divider>

              <!-- 尾部美化 -->
              <div class="bottom-beautify">
                <div style="width: 100%">
                  <p>
                    <el-button
                      @click="setLoading"
                      style="margin-left: 25.5%"
                      icon="el-icon-loading"
                      >重新加载</el-button
                    >
                  </p>
                  <el-skeleton
                    style="width: 100%"
                    :loading="loading"
                    animated
                    :count="3"
                  >
                    <template slot="template">
                      <el-skeleton-item
                        variant="image"
                        style="width: 100%; height: 160px"
                      />
                      <div style="padding: 14px">
                        <el-skeleton-item variant="h3" style="width: 50%" />
                        <div
                          style="
                            display: flex;
                            align-items: center;
                            justify-items: space-between;
                            margin-top: 16px;
                            height: 16px;
                          "
                        >
                          <el-skeleton-item
                            variant="text"
                            style="margin-right: 16px"
                          />
                          <el-skeleton-item variant="text" style="width: 30%" />
                        </div>
                      </div>
                    </template>
                    <template>
                      <el-card
                        :body-style="{ padding: '0px', marginBottom: '1px' }"
                        v-for="item in lists"
                        :key="item.code"
                      >
                        <img
                          :src="item.imgUrl"
                          class="image multi-content"
                          style="width: 100%; height: 160px"
                        />
                        <div style="padding-bottom: 12%">
                          <span>{{ item.name }}</span>
                          <div class="bottom card-header">
                            <span class="time">{{ currentDate }}</span>
                          </div>
                        </div>
                      </el-card>
                    </template>
                  </el-skeleton>
                </div>

                <div
                  style="
                    /* border-top: 3.5px double gainsboro; */
                    border-left: 2.5px double gainsboro;
                    border-right: 2.5px double gainsboro;
                    border-bottom: 3.5px double gainsboro;
                    border-radius: 15px;
                    margin-left: 0.19%;
                    margin-bottom: 15%;
                    margin-top: 8%;
                  "
                >
                  <span style="font-size: 15px; color: pink; margin-left: 15%"
                    >_(:з)∠)_
                  </span>
                  <!-- 用户体验打分 -->
                  <!-- <div style="margin-bottom: 2%">
                    <el-rate style="margin-left: 4%" v-model="value" show-text>
                    </el-rate>
                    <span
                      style="font-size: larger; color: pink; margin-left: 4.5%"
                      >请为我们的网站打分~
                    </span>
                  </div> -->
                </div>
                <!-- <el-divider></el-divider> -->
              </div>
            </el-card>
          </div>

          <!-- 文章卡片父级盒子 -->
          <div class="articlebox">
            <el-timeline>
              <!-- 博客文章循环的每张卡片的盒子 -->
              <el-timeline-item
                :timestamp="item.createdAt"
                placement="top"
                v-for="(item, index) in articles"
                :key="index"
              >
                <!-- 头像背景 -->
                <div class="thumbnail-bg">
                  <el-row>
                    <el-col :span="24">
                      <!-- 缩略图 -->
                      <div class="thumbnail">
                        <el-image
                          style="width: 85px; height: 85px; border-radius: 100%"
                          :src="item.avatar"
                        ></el-image></div
                    ></el-col>
                  </el-row>
                </div>

                <div class="title-right">
                  <span class="title-right-text"
                    >software technology 2 0 软一一组</span
                  >
                </div>

                <!-- 特效 -->
                <div class="main" style="--c: #99bbff"></div>
                <!-- 文章卡片 -->
                <el-card
                  class="box-card"
                  ref="boxcardbg"
                  v-bind:style="boxcardbg"
                >
                  <!-- 卡片头部 -->
                  <div slot="header" class="clearfix" style="height: 8px">
                    <!-- 文章标题 -->
                    <router-link
                      :to="{ path: '/article', query: { id: item.id } }"
                    >
                      <!-- 文章标题 -->
                      <div class="title">{{ item.articleTitle }}</div>
                    </router-link>
                  </div>
                  <!-- 卡片尾部 -->
                  <div class="clear">
                    <!-- 文章内容背景 -->
                    <div class="content-bg">
                      <el-row>
                        <el-col :span="24">
                          <!-- 文章内容 -->
                          <div
                            v-html="item.articleContent"
                            class="content"
                            ref="boxcardtext"
                            v-bind:style="{ color: boxcardtext }"
                          ></div>
                        </el-col>
                      </el-row>
                    </div>
                    <div class="all-bg">
                      <el-row>
                        <el-col :span="24">
                          <!-- 作者 -->
                          <div class="author">
                            {{ item.nickname }}
                          </div>
                          <!-- 点赞数量 -->
                          <div class="praise">
                            <el-button
                              disabled
                              icon="el-icon-thumb"
                              circle
                              class="praise-icon"
                              size="mini"
                            ></el-button>
                            {{ item.articlePraise }}
                          </div>
                          <!-- 评论数量 -->
                          <div class="comment">
                            <el-button
                              disabled
                              icon="el-icon-chat-dot-round"
                              circle
                              class="comment-icon"
                              size="mini"
                            ></el-button>
                            {{ item.commentsNumber }}
                          </div>
                          <!-- 浏览量 -->
                          <div class="browse">
                            <el-button
                              disabled
                              icon="el-icon-view"
                              circle
                              class="browse-icon"
                              size="mini"
                            ></el-button>
                            {{ item.viewstsNumber }}
                          </div>
                        </el-col>
                      </el-row>
                    </div>
                  </div>
                </el-card>
              </el-timeline-item>
            </el-timeline>
          </div>

          <!-- 循环热度榜数据 -->
          <div class="hostbox">
            <!-- 特效 -->
            <div class="right" style="--c: #ffff6f"></div>

            <!-- 热度榜卡片0 -->
            <el-card
              class="box-card-right-top"
              name="boxcardright"
              ref="boxcardright"
              v-bind:style="boxcardright"
            >
              <div slot="header" class="clearfix-right">
                <span class="host-head-text">公告栏</span>
                <!-- 热度榜切换颜色 -->
                <el-switch
                  v-model="value"
                  active-color="#DDDDDD"
                  inactive-color="#999999"
                  class="light"
                  v-bind="hostchangecolor(value)"
                ></el-switch>
              </div>

              <!-- 热度播放图父级盒子 -->
              <!-- <div class="hostphotobox"> -->
              <div class="shell">
                <div v-for="(i, index) in hostphotos" :key="index">
                  <div class="box">
                    <img :src="i.hostphoto" />
                    <span>{{ i.hostname }}</span>
                  </div>
                </div>
              </div>
              <!-- </div> -->

              <!-- 公告栏父级盒子 -->
              <div
                class="announcement"
                style="font-size: 17px; padding-top: 12px; padding-bottom: 15px"
              >
                <span>
                  <span>
                    本网站尊重并保护所有使用服务用户的个人隐私权。为了给您提供更准确、更有个性化的服务，我们会按照本隐私权政策的规定使用和披露您的个人信息。您在同意本网站服务使用协议之时，即视为您已经同意本隐私权政策全部内容...更多请前往网站规章须知详情页面查看
                  </span>
                  <a
                    href="https://blog.csdn.net/qq_23251685/article/details/85344713"
                    target="_blank"
                    >更多详情...</a
                  >
                </span>
              </div>
            </el-card>

            <!-- 文章热度 -->
            <!-- <div v-for="(i, index) in thehostbox" :key="index"> -->
            <div>
              <!-- 特效 -->
              <div class="right-bottom" style="--c: #ffff6f"></div>

              <!-- 热度榜卡片1 -->
              <el-card
                class="box-card-right"
                ref="boxcardrightbg"
                v-bind:style="boxcardrightbg"
              >
                <div slot="header" class="clearfix-right-host">
                  <span style="font-size: 30px">编辑推荐热点</span>
                </div>
                <div v-for="(con, conindex) in announcement" :key="conindex">
                  <div
                    style="
                      border: 5px double #afd9de;
                      padding-left: 8px;
                      padding-right: 5px;
                      padding-top: 8px;
                      padding-bottom: 8px;
                      margin-top: 8px;
                      font-size: 16.4px;
                    "
                  >
                    <span style="padding-bottom: 2px">
                      <!-- {{ conindex }} 、 -->
                      <a :href="con.Id" target="_blank"
                        >{{ con.articleTitle }}
                      </a>
                    </span>
                  </div>
                </div>
              </el-card>
            </div>

            <div>
              <!-- 特效 -->
              <div class="right-bottom" style="--c: #ffff6f"></div>

              <!-- 热度榜卡片2 -->
              <el-card
                class="box-card-right"
                ref="boxcardrightbg"
                v-bind:style="boxcardrightbg"
              >
                <div slot="header" class="clearfix-right-host">
                  <span style="font-size: 32px; margin-left: 20px"
                    >推荐热点</span
                  >
                </div>
                <div
                  v-for="(ii, cconindex) in announcementsecond"
                  :key="cconindex"
                >
                  <div
                    style="
                      border: 5px double #afd9de;
                      padding-left: 8px;
                      padding-right: 5px;
                      padding-top: 8px;
                      padding-bottom: 8px;
                      margin-top: 8px;
                      font-size: 16.4px;
                    "
                  >
                    <span style="padding-bottom: 2px">
                      <!-- {{ cconindex }} 、 -->
                      <a :href="ii.url" target="_blank">{{ ii.title }} </a>
                    </span>
                  </div>
                </div>
              </el-card>
            </div>
          </div>
        </div>
      </div>

      <!-- 到底了提示父级盒子 -->
      <div class="bottomtipbox">
        <div class="bottom-tip">
          <el-divider></el-divider>
          <span class="bottomtext"
            >到底啦 ~ 没有啦 ~ 康康其他内容叭 ~ _(:з)∠)_</span
          >
        </div>
      </div>
    </div>
  </div>
</template>

<style src="../../style/main/main.css" scoped></style>

<script>
import { marked } from "marked";
import dayjs from "dayjs";
import { getArticles } from "@/api/articleApi";
import MenuComponent from "@/components/menu.vue";
import baseUrl from "@/config/appConfig.js";
export default {
  name: "ArtCate",
  inject: ["reload"],
  data() {
    return {
      classifyleft: classifyleftdata, // 分类栏后端分类数据
      classifyright: classifyrightdata, // 分类栏前端分类数据
      drawer: false, // 分类栏初始化是否打开
      direction: "ltl", // 分类栏打开位置
      loading: true, // 分类栏尾部美化初始化是否展示
      lists: [], // 分类栏尾部美化
      currentDate: dayjs(new Date()).format(" YYYY-MM-DD HH:MM:ss"), // 分类栏尾部美化展示的时间
      value: true, // 是否禁用热度榜切换颜色滑块
      hostphotos: hostphotodata, // 热度榜头部播放图
      articles: articledata, // 文章数据
      photos: photosdata, // 首页顶部轮播图
      thehostbox: hostcarddata, // 热点标题
      announcement: announcementcontentdata, // 公告内容
      announcementsecond: announcementseconddata, // 一组的链接
      timevalue: new Date(), //日历时间
      boxcardright: {}, // 公告卡片背景颜色
      boxcardtext: {}, // 文章内容字体
      boxcardbg: {}, // 文章背景颜色
      announcementcontent: {}, // 公告内容字体颜色
      hostcontenttext: {}, // 热度内容字体
      boxcardrightbg: {}, // 热度卡片背景
      // props: {
      //   lazy: true,
      //   lazyLoad(node, resolve) {
      //     const { level } = node;
      //     setTimeout(() => {
      //       const nodes = Array.from({ length: level + 1 }).map((item) => ({
      //         value: ++id,
      //         label: `选项${id}`,
      //         leaf: level >= 2,
      //       }));
      //       // 通过调用resolve将子节点数据返回，通知组件数据加载完成
      //       resolve(nodes);
      //     }, 1000);
      //   },
      // }, // 分类栏内容
    };
  },
  components: {
    MenuComponent,
  },
  // computed: {
  //   compiledMarkdown: function () {
  //     for (let i = 0; i < this.articles.length; i++) {
  //       return marked(this.articles[i].articleContent)
  //     }
  //   },
  // },
  methods: {
    handleClose(done) {
      this.$confirm("确认关闭？")
        .then((_) => {
          done();
        })
        .catch((_) => {});
    },
    setLoading() {
      this.loading = true;
      setTimeout(() => (this.loading = false), 2000);
    },
    hostchangecolor(value) {
      if (value == true) {
        // console.log(value + "热度卡片当前背景色为white");
        // console.log(this.hostcontenttext);
        // this.boxcardright.background = "#F5F5F5" + ` !important`;

        // 文章内容字体
        this.boxcardtext = "#444444";
        // 公告内容字体
        this.announcementcontent = "#444444";
        // 热度内容字体
        this.hostcontenttext = "#F5F5F5";
        // 热度卡片背景
        this.boxcardrightbg.background =
          "linear-gradient(to top, #b2bdc7 0%, rgb(154, 144, 144) 100%)" +
          ` !important`;
        // 热度榜公告背景颜色
        this.boxcardright.background =
          "linear-gradient(to top, #dfe9f3 0%, white 100%)" + ` !important`;
        // 文章背景颜色
        this.boxcardbg.background =
          "linear-gradient(to top, #dfe9f3 0%, white 100%)" + ` !important`;
      } else if (value == false) {
        // console.log(value + "热度卡片当前背景色为black");
        // console.log(this.hostcontenttext);
        // this.boxcardright.background = "#444444" + ` !important`;

        // 文章内容字体
        this.boxcardtext = "#F5F5F5";
        // 公告内容字体
        this.announcementcontent = "#F5F5F5";
        // 热度内容字体
        this.hostcontenttext = "#444444";
        // 热度卡片背景
        this.boxcardrightbg.background =
          "linear-gradient(to top, #dfe9f3 0%, white 100%)" + ` !important`;
        // 热度榜公告背景颜色
        this.boxcardright.background =
          "linear-gradient(to top, #b2bdc7 0%, rgb(154, 144, 144) 100%)" +
          ` !important`;
        // 文章背景颜色
        this.boxcardbg.background =
          "linear-gradient(to top, #c8d4df 0%, rgb(200, 189, 189) 100%)" +
          ` !important`;
      } else {
        console.log("啥也没有改变");
      }
    },
    getKeyword(data) {
      // this.articles = data
      var params = {
        keyWord: data,
      };
      getArticles(params).then((res) => {
        if (res.code == 20000) {
          this.$message.success("查询成功");
          this.articles = res.data.data;
          this.articles.forEach((item) => {
            item.createdAt = dayjs(item.createdAt).format(" YYYY-MM-DD");
            item.avatar = baseUrl.baseUrl + "/Files/" + item.avatar;
            // item.articleContent = marked(item.articleContent);
          });
          console.log(this.articles);
        } else {
          this.$message.warning("查询失败");
        }

        // this.reoload()
      });
    },
  },
  created() {
    getArticles().then((res) => {
      this.articles = res.data.data;
      this.announcement = res.data.data;
      this.articles.forEach((item) => {
        item.createdAt = dayjs(item.createdAt).format(" YYYY-MM-DD");
        item.avatar = baseUrl.baseUrl + "/Files/" + item.avatar;
        // item.articleContent = marked(item.articleContent);
      });
      this.announcement.forEach((item) => {
        item.createdAt = dayjs(item.createdAt).format(" YYYY-MM-DD");
        item.Id = "https://artback.zbmwd.top" + `/article?id=${item.id}`;
        // item.articleContent = marked(item.articleContent);
      });
      console.log(this.announcement);
    });
  },
  mounted() {
    this.loading = false;
    this.lists = listsdata;
  },
};
let id = 0;

let classifyleftdata = [
  {
    code: 0,
    type: "primary",
    name: ".NET",
  },
  {
    code: 1,
    type: "success",
    name: "PHP",
  },
  {
    code: 2,
    type: "info",
    name: "C++",
  },
  {
    code: 3,
    type: "warning",
    name: "Java",
  },
  {
    code: 4,
    type: "danger",
    name: "C语言",
  },
  {
    code: 5,
    type: "",
    name: "其他语言",
  },
];

let classifyrightdata = [
  {
    code: 0,
    type: "primary",
    name: "Html/Css",
  },
  {
    code: 1,
    type: "success",
    name: "jQuery",
  },
  {
    code: 2,
    type: "info",
    name: "Vue",
  },
  {
    code: 3,
    type: "warning",
    name: "HTML5",
  },
  {
    code: 4,
    type: "danger",
    name: "Angular",
  },
  {
    code: 5,
    type: "",
    name: "React",
  },
];

let listsdata = [
  {
    code: 0,
    imgUrl:
      "https://fuss10.elemecdn.com/a/3f/3302e58f9a181d2509f3dc0fa68b0jpeg.jpeg",
    name: "麋鹿",
  },
  {
    code: 1,
    imgUrl:
      "https://fuss10.elemecdn.com/1/34/19aa98b1fcb2781c4fba33d850549jpeg.jpeg",
    name: "骏马",
  },
  {
    code: 2,
    imgUrl:
      "https://fuss10.elemecdn.com/0/6f/e35ff375812e6b0020b6b4e8f9583jpeg.jpeg",
    name: "山狮",
  },
  {
    code: 3,
    imgUrl:
      "https://fuss10.elemecdn.com/d/e6/c4d93a3805b3ce3f323f7974e6f78jpeg.jpeg",
    name: "狐狸",
  },
  {
    code: 4,
    imgUrl:
      "https://fuss10.elemecdn.com/3/28/bbf893f792f03a54408b3b7a7ebf0jpeg.jpeg",
    name: "雪狐",
  },
  {
    code: 5,
    imgUrl:
      "https://fuss10.elemecdn.com/9/bb/e27858e973f5d7d3904835f46abbdjpeg.jpeg",
    name: "小鹿",
  },
  // {
  //   code: 6,
  //   imgUrl:
  //     "https://fuss10.elemecdn.com/2/11/6535bcfb26e4c79b48ddde44f4b6fjpeg.jpeg",
  //   name: "猿猴",
  // },
];

let announcementseconddata = [
  {
    code: 0,
    url: "https://www.cnblogs.com/chanshuyi/p/how-to-design-a-second-killing-system.html",
    title: "从全局角度，如何设计一个秒杀系统？",
  },
  {
    code: 1,
    url: "https://www.cnblogs.com/huangxincheng/p/16492698.html",
    title: "聊一聊 C# 后台 GC 到底是怎么回事？",
  },
  {
    code: 2,
    url: "https://www.cnblogs.com/hong-fithing/p/16391399.html",
    title: "从工程师到技术 leader 思维升级",
  },
  {
    code: 3,
    url: "https://www.cnblogs.com/coco1s/p/16489468.html",
    title: "超酷炫的转场动画？CSS 轻松拿下！",
  },
  {
    code: 4,
    url: "https://www.cnblogs.com/stoneFang/p/16480628.html",
    title: "可落地的DDD(7)-战术设计上的一些误区",
  },
  {
    code: 5,
    url: "https://www.cnblogs.com/chanshuyi/p/how-to-build-the-stable-system.html",
    title: "系统总出故障怎么办，或许你该学学稳定性建设！",
  },
  // {
  //   code: 6,
  //   url: "https://www.cnblogs.com/lulianqi/p/16476166.html",
  //   title: "HTTPS 如何保证数据传输安全",
  // },
  {
    code: 7,
    url: "https://www.cnblogs.com/xiahj/p/flvExtend.html",
    title: `flv.js的追帧、断流重连及实时更新的直播优化方案`,
  },
  // {
  //   code: 8,
  //   url: "https://www.cnblogs.com/coco1s/p/16472985.html",
  //   title: "利用噪声构建美妙的 CSS 图形",
  // },
  // {
  //   code: 9,
  //   url: "https://www.cnblogs.com/sealio/p/16497757.html",
  //   title: "21条最佳实践，全面保障 GitHub 使用安全",
  // },
  // {
  //   code: 10,
  //   url: "https://www.cnblogs.com/shengshengwang/p/16500644.html",
  //   title: "基于ABP实现DDD--领域服务、应用服务和DTO实践",
  // },
];

let announcementcontentdata = [
  {
    code: 0,
    url: "https://www.cnblogs.com/chanshuyi/p/how-to-design-a-second-killing-system.html",
    title: "从全局角度，如何设计一个秒杀系统？",
  },
  {
    code: 1,
    url: "https://www.cnblogs.com/huangxincheng/p/16492698.html",
    title: "聊一聊 C# 后台 GC 到底是怎么回事？",
  },
  {
    code: 2,
    url: "https://www.cnblogs.com/hong-fithing/p/16391399.html",
    title: "从工程师到技术 leader 思维升级",
  },
  {
    code: 3,
    url: "https://www.cnblogs.com/coco1s/p/16489468.html",
    title: "超酷炫的转场动画？CSS 轻松拿下！",
  },
  {
    code: 4,
    url: "https://www.cnblogs.com/stoneFang/p/16480628.html",
    title: "可落地的DDD(7)-战术设计上的一些误区",
  },
  {
    code: 5,
    url: "https://www.cnblogs.com/chanshuyi/p/how-to-build-the-stable-system.html",
    title: "系统总出故障怎么办，或许你该学学稳定性建设！",
  },
  // {
  //   code: 6,
  //   url: "https://www.cnblogs.com/lulianqi/p/16476166.html",
  //   title: "HTTPS 如何保证数据传输安全",
  // },
  {
    code: 7,
    url: "https://www.cnblogs.com/xiahj/p/flvExtend.html",
    title: `flv.js的追帧、断流重连及实时更新的直播优化方案`,
  },
  // {
  //   code: 8,
  //   url: "https://www.cnblogs.com/coco1s/p/16472985.html",
  //   title: "利用噪声构建美妙的 CSS 图形",
  // },
  // {
  //   code: 9,
  //   url: "https://www.cnblogs.com/sealio/p/16497757.html",
  //   title: "21条最佳实践，全面保障 GitHub 使用安全",
  // },
  // {
  //   code: 10,
  //   url: "https://www.cnblogs.com/shengshengwang/p/16500644.html",
  //   title: "基于ABP实现DDD--领域服务、应用服务和DTO实践",
  // },
];

// 文章内容
let articledata = [
  {
    code: 0,
    articleTitle: "震惊！此处为文章标题",
    avatar: require("../../assets/images/login_avatar.jpg"),
    articleContent: "震惊！此处为文章内容",
    nickname: "此处为文章作者",
    praise: "66.6 w",
    commentsNumber: "233 w",
    viewstsNumber: "666 w",
    createdAt: "2022-07-17 18:28:30",
  },
  {
    code: 1,
    articleTitle: "震惊！此处为文章标题",
    avatar: require("../../assets/images/login_avatar.jpg"),
    articleContent: "震惊！此处为文章内容",
    nickname: "此处为文章作者",
    praise: "66.6 w",
    commentsNumber: "233 w",
    viewstsNumber: "666 w",
    createdAt: "2022-07-17 18:28:30",
  },
  {
    code: 2,
    articleTitle: "震惊！此处为文章标题",
    avatar: require("../../assets/images/login_avatar.jpg"),
    articleContent: "震惊！此处为文章内容",
    nickname: "此处为文章作者",
    praise: "66.6 w",
    commentsNumber: "233 w",
    viewstsNumber: "666 w",
    createdAt: "2022-07-17 18:28:30",
  },
];

// 首页头部轮播图
let photosdata = [
  { code: 0, codeview: require("../../assets/images/login_bg.jpg") },
  { code: 1, codeview: require("../../assets/images/login_bg2.0.jpg") },
  { code: 2, codeview: require("../../assets/images/img (1).jpg") },
  { code: 3, codeview: require("../../assets/images/img (2).jpg") },
  { code: 4, codeview: require("../../assets/images/img (3).jpg") },
  { code: 5, codeview: require("../../assets/images/img (4).jpg") },
];

// 热度榜头部播放图
let hostphotodata = [
  {
    code: 0,
    hostname: "spring",
    hostphoto: require("../../assets/images/img (2).jpg"),
  },
  {
    code: 1,
    hostname: "summer",
    hostphoto: require("../../assets/images/img (4).jpg"),
  },
  {
    code: 2,
    hostname: "autumn",
    hostphoto: require("../../assets/images/img (3).jpg"),
  },
  {
    code: 3,
    hostname: "winter",
    hostphoto: require("../../assets/images/img (1).jpg"),
  },
];

// 公告栏内容
// var announcementcontentdata = [
//   {
//     code: 0,
//     announcementcontent: "震惊！此处是公告内容",
//   },
// ];

// 热点标题
let hostcarddata = [
  {
    code: 0,
    hosttitle: "震惊！此处为热点标题",
    hostcontent: "震惊！此处为热点内容",
  },
];
</script>
