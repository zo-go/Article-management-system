export function getMenu(config) {
    // console.log(config);
    // console.log(JSON.parse(config.body))
    // 先判断用户是否存在
    // 判断账号和密码是否对应
    if (config.username === 'admin' && config.password === '123456') {
        return {
            code: 20000,
            data: {
                menu: [
                    {
                        path: "/home",
                        children: null,
                        icon: "el-icon-s-home",
                        indexPath: "/home",
                        title: "首页",
                    },
                    {
                        path: "/users",
                        children: null,
                        icon: "el-icon-s-custom",
                        indexPath: "/users",
                        title: "用户管理",
                    },
                    {
                        icon: "el-icon-s-order",
                        indexPath: "2",
                        title: "文章管理",
                        children: [
                            {
                                path: "/art-list",
                                icon: "el-icon-document-copy",
                                indexPath: "/art-list",
                                title: "文章列表",
                            },
                            {
                                path: "/art-cate",
                                title: "文章分类",
                                icon: "el-icon-s-data",
                                indexPath: "/art-cate",
                            },
                        ],
                    },
                    {
                        path: "/userInfo",
                        title: "个人中心",
                        icon: "el-icon-s-data",
                        indexPath: "/UserInfo",
                        children: [
                            {
                                path: "/user-info",
                                icon: "el-icon-s-operation",
                                indexPath: "/user-info",
                                title: "基本资料",
                            },
                        ],
                    },
                ],
                message: '获取成功'
            }
        }
    } else {
        return {
            code: 20000,
            data: {
                menu: [
                    {
                        path: "/home",
                        children: null,
                        icon: "el-icon-s-home",
                        indexPath: "/home",
                        title: "首页",
                    },
                    {
                        path: "/userInfo",
                        title: "个人中心",
                        icon: "el-icon-s-data",
                        indexPath: "/UserInfo",
                        children: [
                            {
                                path: "/user-info",
                                icon: "el-icon-s-operation",
                                indexPath: "/user-info",
                                title: "基本资料",
                            },
                        ],
                    },
                ],
                message: '获取成功'
            }
        }
    }
}
