Asserts

--Editor 自写的灵活方便插件
 
--Editor_NGUI 较大型三方的插件(前面加上Editor是为了让所有编辑器都集中在一块)
 
--Editor_WaypointTool 较大型三方的插件
 
--Gizmos 使用GIZMOS所需要的标志等临时文件
 
--StreamingAssets （包括JsonData目录/Assetbundle目录/各种只读文件预储存目录,这个目录将自动打包到导出程序，用Application.streamingAssetsPath读取)
 
--Models 模型文件，其中会包括自动生成的材质球文件
 
--Others 其他类型的文件（一般作为不常修改的文件类型，例如添加的Shader、物理材质、动画文件。）若认为是比较常用的话，可以提取成一个文件夹。
 
--Plugins 主要是DLL的外部插件文件，例如JsonFx、SmartFoxClient等
 
--Prefabs 预储存文件
 
--Resources 动态加载的资源文件，一般这里只用少量，多的话，需要自己打ASSETBUNDLE包，有选择性的动态加载
 
--Scenes 场景文件
 
--Scripts 脚本代码文件
 
--Sounds 音效文件
 
--Textures 所有的贴图
 
--Z_Test 临时测试文件,加Z是让它放到最下面。放到一起的好处是删除的时候可以任意直接删掉，不会影响其他部分的东西。
 
由于没有对4.0的动画系统有很深入了解，所以这结构会待有了更好了解4.0动画系统再作修改。项目目前也没有用到4.0最新的动画系统。