<template>
   <div class="container">
    <!-- 面包屑导航 -->      
    <div class="warp-breadcrum">   
      <!-- <el-breadcrumb separator="/">
          <el-breadcrumb-item :to="{ path: '/' }"><b>首页</b></el-breadcrumb-item>
          <el-breadcrumb-item>个人管理</el-breadcrumb-item>
          <el-breadcrumb-item>个人信息</el-breadcrumb-item>
      </el-breadcrumb>          -->
    </div>
    <!-- 下方主内容 --> 
    <div class="warp-body">
      <!-- 工具栏 -->    
      <div class="toolbal"> 
        <!-- <el-form :inline="true" style="margin-bottom:15px">
          <el-button v-if="!isModify" type="primary" @click="isModify = true">编辑信息</el-button>
          <el-button v-if="isModify" type="primary" @click="isModify = false">取消编辑</el-button>          
        </el-form>               -->
        <div class="topSearch" :hidden="!IsSearch">
          <div class="picTop"></div>
          <el-input placeholder="请输入搜索内容" v-model="searchValue" class="input-with-select-top">
              <el-button slot="append" icon="el-icon-search" @click="search()"></el-button>
          </el-input>
        </div>
        
      </div>
      <!-- 表格区 --> 
      <div class="main-data"> 
        <div class="centerDiv" :hidden="IsSearch">
          <div class="pic"></div>
          <div class="control-group">
            <el-input placeholder="请输入搜索内容" v-model="searchValue" class="input-with-select" style="border:none;">
              <el-button slot="append" icon="el-icon-search" @click="search()"></el-button>
            </el-input>
          </div> 
        </div>
          <el-table  class="table" :data="awdData" style="width:100%" v-loading="listLoading" height="string" :hidden="!IsSearch"> 
            <el-table-column prop="AwdeeName" label="获奖人姓名" width="120">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdeeName)">{{scope.row.AwdeeName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdeeName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdName" label="获奖名称" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdName)">{{scope.row.AwdName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdeeOrgName" label="获奖人所属学院" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdeeOrgName)">{{scope.row.AwdeeOrgName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdeeOrgName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdeeBranch" label="团支部" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdeeBranch)">{{scope.row.AwdeeBranch}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdeeBranch}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdOrgName" label="获奖学院" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdOrgName)">{{scope.row.AwdOrgName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdOrgName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdProName" label="项目名称" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdProName)">{{scope.row.AwdProName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdProName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdYear" label="获奖年份" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdYear)">{{scope.row.AwdYear}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdYear}}</span>
              </template>
            </el-table-column>
          </el-table>
          <el-table  class="table" :data="hnrData" style="width:100%" v-loading="listLoading" height="string" :hidden="!IsSearch">
            <el-table-column prop="AwdeeName" label="获奖人姓名" width="120">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdeeName)">{{scope.row.AwdeeName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdeeName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="HnrName" label="荣誉名称" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.HnrName)">{{scope.row.HnrName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.HnrName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdeeOrgName" label="获奖人所属学院" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdeeOrgName)">{{scope.row.AwdeeOrgName}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdeeOrgName}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="AwdeeBranch" label="团支部" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.AwdeeBranch)">{{scope.row.AwdeeBranch}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.AwdeeBranch}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="HnrAnnual" label="获奖年度" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.HnrAnnual)">{{scope.row.HnrAnnual}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.HnrAnnual}}</span>
              </template>
            </el-table-column>
            <el-table-column prop="HnrTime" label="获奖时间" width="180">
              <template scope="scope">
                <span v-if="cellClassName(scope.row.HnrTime)">{{scope.row.HnrTime}}</span>
                <span v-else :style="keyWordStyle">{{scope.row.HnrTime}}</span>
              </template>
            </el-table-column>
        </el-table>
      </div>
    </div>
  </div>
</template>

<script type="text/ecmascript-6">
import PubMethod from "../common/util";
import {
  reqGetAccountInfo,
  posModifyAccountInfo,
  selectByKeyWord
} from "../api/api";
export default {
  data() {
    return {
      // 是否开启编辑
      isModify: false,
      // 主信息区
      listLoading: false,
      accountData: [],
      select: "",
      searchValue: "",
      keyWord: "",
      IsSearch: false,
      awdData: [],
      hnrData: [],

      keyWordStyle: 'color: #F4BA26; font-weight: bold;',
    };
  },
  created() {},
  methods: {
    search() {
      if (this.searchValue != "") {
        this.keyWord = this.searchValue;
        this.IsSearch = true;
        let Get_SelectByKeyWord_param = {
          access_token: this.$store.state.access_token,
          keyWord: this.searchValue
        };
        selectByKeyWord(Get_SelectByKeyWord_param)
          .then(res => {
            console.log(res);
            if (res.data.status == "success") {
              this.awdData = res.data.data.awdList;
              this.hnrData = res.data.data.hnrList;
              this.$message({
                message: res.data.messages,
                type: "success"
              });
            } else {
              this.$message.error(res.data.messages);
            }
          })
          .catch(res => {});
      } else {
        this.IsSearch = false;
        this.$message("搜索内容不能为空");
      }
    },
    cellClassName(param) {
      console.log(param);
      if (param.indexOf(this.keyWord) == -1) {
        return true;
      } else {
        return false;
      }
    }
  }
};
</script>

<style scoped lang="scss">
.main-data {
  display: flex;
  justify-content: space-around;
  align-items: flex-start;
  position: relative;
}
.control-group {
  padding-top: 0px;
  padding-bottom: 0px;
  margin-top: 20px;
  width: 880px;
  background: rgba(255, 255, 255, 0.9);
  border: 0px solid #eaeaea;
  box-shadow: 0 0 25px #cac6c6;
  display: flex;
  flex-direction: column;
  > h1 {
    padding-left: 30px;
    padding-bottom: 20px;
    border-bottom: 1px solid #000;
  }
  .control-label {
    padding-left: 30px;
    font-weight: bold;
    font-size: 1.2em;
    line-height: 2;
    font-family: Helvetica, Arial, sans-serif;
    margin-bottom: 5px;
  }
  .contorl {
    padding-left: 30px;
    color: #222;
    font-family: Helvetica, Arial, sans-serif;
    font-size: 14px;
    line-height: 19px;
    direction: ltr;
    padding-bottom: 15px;
  }
  .button {
    font-weight: bold;
    padding-left: 15px;
    text-align: center;
  }
}

.input-with-select {
  border: 0px;
  background-color: #fff;
}

.input-with-select-top {
  border: 0px;
  background-color: #fff;
  width: 500px;
  float: left;
  margin-top: 13px;
  margin-left: 15px;
  box-shadow: 0 0 25px #cac6c6;
}

.pic {
  width: 465px;
  height: 201px;
  //display: flex;
  margin-left: auto;
  margin-right: auto;
  float: bottom;
  background-image: url("../assets/searchLogo_big.png");
  background-size: 100% 100%;
}
.centerDiv {
  position: absolute;
  top: 30%;
  transform: translateY(-50%);
}
.picTop {
  width: 116px;
  height: 50px;
  //display: flex;
  //margin-left: auto;
  margin-right: auto;
  float: left;
  background-image: url("../assets/searchLogo_big.png");
  background-size: 100% 100%;
}
.centerDiv {
  position: absolute;
  top: 30%;
  transform: translateY(-50%);
}
.topSearch {
  margin-bottom: 80px;
}
</style>
