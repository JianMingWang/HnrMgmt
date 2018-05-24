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
        <div :hidden="!IsSearch">
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
        <el-table class="table" :data="awdData" style="width:100%" v-loading="listLoading" height="string" :hidden="!IsSearch"> 
            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column prop="AwdeeName" label="获奖人姓名" width="180" />
            <el-table-column prop="AwdName" label="获奖名称" width="180" />
            <el-table-column prop="AwdOrgName" label="所属学院" width="180" />
            <el-table-column prop="ApplyTime" label="申请时间" width="220" />
          </el-table>
        <el-table class="table" :data="hnrData" style="width:100%" v-loading="listLoading" height="string" :hidden="!IsSearch"> 
            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column prop="AwdeeName" label="获奖人姓名" width="180" />
            <el-table-column prop="HnrName" label="荣誉名称" width="180" />
            <el-table-column prop="AwdeeOrgName" label="所属学院" width="180" />
            <el-table-column prop="ApplyTime" label="申请时间" width="220" />
        </el-table>
      </div>
    </div>
  </div>
</template>

<script type="text/ecmascript-6">
import PubMethod from "../common/util";
import { reqGetAccountInfo, posModifyAccountInfo } from "../api/api";
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
      IsSearch: false,
    };
  },
  created() {},
  methods: {
    search() {
      if(this.searchValue != ""){
        this.IsSearch = true;
      }else{
        this.IsSearch = false;
        this.$message('搜索内容不能为空');
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

.input-with-select{
  border: 0px;
  background-color: #fff;
}

.input-with-select-top{
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
</style>
