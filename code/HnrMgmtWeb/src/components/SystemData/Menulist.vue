<template>
  <div class="container">
    <div class="warp-breadcrum">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item :to="{ path: '/' }"><b>首页</b></el-breadcrumb-item>
        <el-breadcrumb-item>系统管理</el-breadcrumb-item>
        <el-breadcrumb-item>接口管理</el-breadcrumb-item>
      </el-breadcrumb>      
    <!-- 面包屑导航 -->      
    </div>
    <div class="warp-body">
    <!-- 下方主内容 -->     
      <div class="toolbal">
      <!-- 工具栏 --> 
        <el-form :inline="true" style="margin-bottom:15px">
          <el-button type="primary" @click="addFormVisible = true" >记录接口</el-button>
        </el-form>             
      </div>
      <div class="main-data">
        <!-- 表格区 -->        
          <el-table class="table" :data="APIPwd" border style="width:100%" v-loading="listLoading" height="string" > 
            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column type="index" width="65" label="序号" style="text-aligin:center" align="center"></el-table-column>
            <el-table-column prop="Name" label="接口名" sortable align="center" ></el-table-column>
            <el-table-column prop="Url" label="Url" sortable align="center" ></el-table-column>
            <el-table-column prop="Priority" label="优先级" sortable align="center" ></el-table-column>
            <el-table-column label="操作" width="200" align="center">
              <template slot-scope="scope">
                <el-button  size="small"  >编辑</el-button>
                <el-button type="danger" size="small"   >删除</el-button>
              </template>
            </el-table-column>
          </el-table>          
      </div>
      <el-pagination layout="total, prev, pager, next, sizes, jumper" @size-change="SizeChangeEvent" @current-change="CurrentChangeEvent" :page-size="size" :page-sizes="[10,15,20,25,30]":total="totalNum">
      </el-pagination>      
    </div>

    <!-- 新增表单 -->
    <el-dialog title="新增API信息" :visible.sync="addFormVisible" v-loading="submitLoading" >
      <el-form :model="addFormBody" label-width="80px" ref="addForm" :rules="rules" auto>
        <el-form-item label="接口名" prop="Name">
          <el-input v-model="addFormBody.Name" placeholder="请输入接口名"  ></el-input>
        </el-form-item>  
        <el-form-item label="Url" prop="Url">
          <el-input v-model="addFormBody.Url" placeholder="请输入Url"  ></el-input>
        </el-form-item>      
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click.native=" addFormVisible = false">取消</el-button>
        <el-button type="primary" @click.native="addSubmit" >提交</el-button>
      </div>
    </el-dialog>

    <!-- 编辑表单 -->
    <el-dialog title="编辑API信息" :visible.sync="modifyFormVisible" v-loading="modifyLoading">
      <el-form :model="modifyFromBody" label-width="80px" ref="modifyFrom" :rules="rules" >
        <el-form-item label="接口名" prop="Name">
          <el-input v-model="modifyFromBody.Name" placeholder="请输入接口名"  ></el-input>
        </el-form-item>  
        <el-form-item label="Url" prop="Url">
          <el-input v-model="modifyFromBody.Url" placeholder="请输入Url"  ></el-input>
        </el-form-item> 
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click.native=" modifyFormVisible = false">取消</el-button>
        <el-button type="primary" @click.native="modifySubmit" >提交</el-button>
      </div>     
    </el-dialog>    
  </div>
</template>

<script type="text/ecmascript-6">
import {reqGetApiList,posAddApi,posModifyApi,reqDeleteApi} from '../../api/api'
import PubMethod from '../../common/util'
 export default {
   data() {
     return {
       // 用户令牌
       access_token:'',
       // 表格数据
       APIPwd: [],
       listLoading:false,

       selectRowIndex:'',
       totalNum:0,
       page:1,
       size:10,

       // 新增API表单规则验证
       rules:{
         Name:[
           {required: true, message: '请输入接口名' , trigger: 'blur'}
         ],
         Url:[
           {required: true, message: '请输入接口地址' , trigger: 'blur' },
         ],      
       },

      // 新增表单相关数据
       submitLoading:false,       
       addFormVisible: false,
       addFormBody:{
         Name:'',
         Url:'',
       },
       
       //编辑表单相关数据
       modifyFormVisible:false,
       modifyLoading:false,
       modifyFromBody:{
         Name:'',
         Url:'',
       }


     }

   },
   //声明周期调用
   mounted(){
     this.getList();
   },

   //方法集合
   methods:{
     //获取已存API列表
     getList(){
       this.listLoading=true
       let param={
           access_token : "11",
           page : this.page,
           size : this.size
           }
           reqGetApiList(param).then((res)=>{
               this.APIPwd = res.data.data.list
               this.totalNum = res.data.data.count;
               //console.log(this.APIPwd)
               this.listLoading=false
               }).catch((res)=>{
                   console.log(res)
                   })
            },
     //新增API接口
     addSubmit(){
       let param
       this.$refs['addForm'].validate((valid)=>{
         if(valid){
           this.submitLoading=true
           //复制字符串
           let para = Object.assign({}, this.addFormBody);
           para.access_token='terry'
           posAddApi(para).then((res)=>{
              this.submitLoading=false
            //公共提示方法，传入当前的vue以及res.data
            PubMethod.statusinfo(this,res.data)
              this.$refs['addForm'].resetFields();
              this.addFormVisible = false;
              this.getList();
           })           
         }
       })
     },
     //显示编辑
     showModifyDialog (index,row) {
       this.modifyFormVisible=true
       this.modifyFromBody= Object.assign({},row)
       this.selectRowIndex=index
       //console.log(this.selectRowIndex)
       },
    //保存编辑
    modifySubmit(){
      this.$refs['modifyFrom'].validate((valid)=>{
        if(valid){
          this.modifyLoading=true;
          let para = Object.assign({},this.modifyFromBody)
          para.access_token='terry'

          posModifyApi(para).then((res)=>{
            //公共提示方法，传入当前的vue以及res.data
            PubMethod.statusinfo(this,res.data)
              this.$refs['modifyFrom'].resetFields()
              this.modifyFormVisible=false
              this.modifyLoading=false
              this.getList()
          })
        }
      })
    },
    //删除API信息
    delectAccTch(index,row){
      this.$confirm('此操作将永久删除该API, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
        }).then(() => {
          let para={ID:row.ID}
          para.access_token='terry'
         
          reqDeleteApi(para).then((res)=>{
            //公共提示方法，传入当前的vue以及res.data
            PubMethod.statusinfo(this,res.data)
            this.getList()
          })

            }).catch(() => {
              this.$message({
                type: 'info',
                message: '已取消删除'
                });          
              });
    },
    //更换每页数量
    SizeChangeEvent(val){
        this.loading=true;
        this.size = val;
        this.getList();
        PubMethod.logMessage(this.page + "   " + this.size);
    },
    //页码切换时
    CurrentChangeEvent(val){
        this.loading=true;
        this.page = val;
        this.getList();
        PubMethod.logMessage(this.page + "   " + this.size);
    }
  }
 }
</script>

<style scoped lang="scss">
    .el-pagination{
        text-align: right;
    }
</style>
