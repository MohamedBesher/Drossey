var colWidth;
var dynamicColWidth;

$(function(){
      $('.close_msg').click(function() {
        $(".notification_msg").remove();
    });
 $(".blue_bg").css("display","none");
       
         //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
  

               if(data.title != undefined  && data.inside_title != undefined && data.pageHeader != undefined ){
                        $('#title').append('<h4>' + data.title + '</h4><hr/><h5>'+data.inside_title+'</h5>');  
                        $('#lesson_title').append(data.pageHeader);
                    }      
                       
                     $("#noti_msg").append(data.prompt);
                //draw flowchart
                drawFlowChart();
                //load first tab content
               /* loadTabContent(0,0);*/

         };
        
      });             
  
    
function drawFlowChart()
{
    //set flowchart title
    $("#flowchart_maintitle").append(data.main_title);
    //set flowchart sub title
    if(data.sub_titles.length == 2 )
        {
            colWidth=6;
           
        }
    else if(data.sub_titles.length  == 3)
        {
            colWidth=4;
            
        }
    else if(data.sub_titles.length  == 4)
        {
            colWidth=3;
            
        }
    $(data.sub_titles).each(function(index,value){
       
        $(".sub_title_arrow").append('<div class="col-xs-'+colWidth+' text-center "><p class="btn"><span class="glyphicon glyphicon-arrow-down"></span></p></div>');
         //append sub title
         $(".sub_title").append(' <div class="col-md-'+colWidth+' text-center col-xs-12"><p id="'+index+'" class="center-block" onclick="loadDynamicTitle('+index+')"><span  style="background: #149fd3;color:#fff;" class="btn btn-lg">'+value.sub_title+'</span></p><p class="btn center-block"><span class="glyphicon glyphicon-arrow-down"></span></p><p class="bg-success text-success btn" style="width:100%"></p><div class="row arrow'+index+'"></div><div class="row dynamic_title'+index+'"></div></div>');
         
        //detremin no of arrow of dynamic data
       /* if(value.dynamicData.length == 1 )
        {
            dynamicColWidth=12;
           
        }
        else if(value.dynamicData.length == 2)
        {
              dynamicColWidth=6;  
        }
        else if(value.dynamicData.length  == 3)
        {
            dynamicColWidth=4;
            
        }
        else if(value.dynamicData.length  == 4)
        {
            dynamicColWidth=3;
            
        }
        //loop on dynamic data
        $(value.dynamicData).each(function(i,v){
           
            
            //draw arrow of dynamic data
             $(".arrow"+index).append('<div class="col-xs-'+dynamicColWidth+' text-center"><p class="btn"><span class="glyphicon glyphicon-arrow-down"></span></div>');
            //draw dynamic title
            if(activeFlag == 0)
                {
                     $(".dynamic_title"+index).append('<div class="col-xs-'+dynamicColWidth+'"><p class="center-block" onclick="loadTab('+index+','+i+')"><span id="'+index+i+'" class="btn  btn-danger active btn-lg">'+v.title+'</span></p></div>');
                    activeFlag = 1;
                }
            else{
                $(".dynamic_title"+index).append('<div class="col-xs-'+dynamicColWidth+'"><p class="center-block" onclick="loadTab('+index+','+i+')"><span id="'+index+i+'" class="btn btn-success btn-lg">'+v.title+'</span></p></div>');   
            }
            
            
        });*/
        
    });
                                      
                                      
                                      
                                     
}
function loadDynamicTitle(index)
{
    var activeFlag=0;
    //debugger;
            //detremin no of arrow of dynamic data
        if(data.sub_titles[index].dynamicData.length == 1 )
        {
            dynamicColWidth=12;
           
        }
        else if(data.sub_titles[index].dynamicData.length == 2)
        {
              dynamicColWidth=6;  
        }
        else if(data.sub_titles[index].dynamicData.length  == 3)
        {
            dynamicColWidth=4;
            
        }
        else if(data.sub_titles[index].dynamicData.length  == 4)
        {
            dynamicColWidth=3;
            
        }
        //loop on dynamic data
        $(data.sub_titles[index].dynamicData).each(function(i,v){
           
            
            //draw arrow of dynamic data
             $(".arrow"+index).append('<div class="col-xs-'+dynamicColWidth+' text-center "><p class="btn"><span class="glyphicon glyphicon-arrow-down"></span></div>');
            //draw dynamic title
            if(activeFlag == 0)
                {
                     $(".dynamic_title"+index).append('<div class="col-xs-'+dynamicColWidth+'"><p class="center-block" onclick="loadTab('+index+','+i+')"><span id="'+index+i+'" class="btn  btn-danger active btn-lg">'+v.title+'</span></p></div>');
                    activeFlag = 1;
                    loadTab(index,i);
                }
            else{
                $(".dynamic_title"+index).append('<div class="col-xs-'+dynamicColWidth+'"><p class="center-block" onclick="loadTab('+index+','+i+')"><span id="'+index+i+'" class="btn btn-success btn-lg">'+v.title+'</span></p></div>');   
            }
            
            
        });
        //append blue_bg to load tabs
        $(".blue_bg").css("display","block");
        //loadTabContent(0,0);
    $("#"+index).css("pointer-events", "none");
    
}
function loadTab(s,d,element)
{
    $(".active").removeClass("btn-danger").addClass("btn-success")
    $("#"+s+d).removeClass("btn-success").addClass("btn-danger").addClass("active");
    loadTabContent(s,d);
}
//s==sub title index, d== dynamic title index
function loadTabContent(s,d){
  
    if(data.sub_titles[s].dynamicData[d].type == 'text'){
        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12"><h4 class="text-line">'+data.sub_titles[s].dynamicData[d].paragraph+'</h4></div>');
    
    }
    else if(data.sub_titles[s].dynamicData[d].type == 'textWithImage'){
        $('.blue_bg .row').html('<div class="col-md-8 col-sm-8 col-xs-12"><h4 class="text-line">'+data.sub_titles[s].dynamicData[d].paragraph+'</h4></div><div class="col-md-4 col-sm-4 col-xs-12 text-center" ><img src="'+data.sub_titles[s].dynamicData[d].mediaSource+'"/></div>');
    
    }
    else if(data.sub_titles[s].dynamicData[d].type == 'textWithVideo'){
        $('.blue_bg .row').html('<div class="col-md-6 col-sm-6 col-xs-12"><h4 class="text-line">'+data.sub_titles[s].dynamicData[d].paragraph+'</h4></div><div class="col-md-6 col-sm-6 col-xs-12 text-center" ><video autoplay controls style="width:100%"><source src="'+data.sub_titles[s].dynamicData[d].mediaSource+'.ogv" type="video/ogg"/><source src="'+data.sub_titles[s].dynamicData[d].mediaSource+'.mp4" type="video/mp4"/></video></div>');
    
    }
    else if(data.sub_titles[s].dynamicData[d].type == 'TextWithSlider'){
        $('.blue_bg .row').html('<div class="col-md-8 col-sm-8 col-xs-12"><h4 class="text-line">'+data.sub_titles[s].dynamicData[d].paragraph+'</h4></div><div class="col-md-4 col-sm-4 col-xs-12 text-center" ><div class="media_cont"></div></div>');
        
        loadSlider(s,d);
    
    }
  else if(data.sub_titles[s].dynamicData[d].type == 'slider'){
        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><div class="media_cont"></div></div>');
        
        loadSlider(s,d);
        
    }
    else if(data.sub_titles[s].dynamicData[d].type == 'video'){
        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><video autoplay controls style="width:100%"><source src="'+data.sub_titles[s].dynamicData[d].mediaSource+'.ogv" type="video/ogg"/><source src="'+data.sub_titles[s].dynamicData[d].mediaSource+'.mp4" type="video/mp4"/></video></div>');
    
    }
    else if(data.sub_titles[s].dynamicData[d].type == 'image'){
        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><img src="'+data.sub_titles[s].dynamicData[d].mediaSource+'"/></div>');
    }   
}
//to load slider on screen
function loadSlider(newS,newD){
    $('.media_cont').append('<div id="carousel" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a class="left carousel-control" href="#carousel" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>');
            
    for(var nn = 0 ; nn < data.sub_titles[newS].dynamicData[newD].mediaSource.length ; nn++){
        $('.carousel ol').append('<li data-target="#carousel" data-slide-to="'+nn+'"></li>');
        $('.carousel .carousel-inner').append('<div class="item"><img src="'+data.sub_titles[newS].dynamicData[newD].mediaSource[nn]+'"></div>');
    }
    
    $('.carousel li:nth-child(1), .carousel .carousel-inner div:nth-child(1)').addClass('active');    
}
function loadScript(path, callback) {
 
    var done = false;
    var scr = document.createElement('script');
    scr.onload = handleLoad;
    scr.onreadystatechange = handleReadyStateChange;
    scr.onerror = handleError;
    scr.src = path;
    document.body.appendChild(scr);

    function handleLoad() {
        if (!done) {
            done = true;
            callback(path, "ok");
        }
    }

    function handleReadyStateChange() {
        var state;
        if (!done) {
            state = scr.readyState;
            if (state === "complete") {
                handleLoad();
            }
        }
    }

    function handleError() {
        if (!done) {
            done = true;
            callback(path, "error");
        }
    }
    return scr;
}
