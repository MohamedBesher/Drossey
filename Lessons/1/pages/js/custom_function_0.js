var cAnswerSoundPath="../sound/Quiz_right"//set the path of correct sound without extention 
,wAnswerSoundPath="../sound/Quiz_wrong"//set the path of wrong sound without extention 
,clickSoundPath="../sound/Speech_Misrecognition"//set the path of nav sound without extention 
,checkSoundPath="../sound/Speech_On" //set the path of ckeck or view sound without extention 
,dragSoundPath="../sound/Speech_On"
,dropSoundPath="../sound/Speech_Off"
,audioTap;
var currFileName =function() {
 //   var pageName=document.location.pathname.match(/[^\/]+$/)[0]    
      var pageName=location.href.split("/").slice(-1)[0]
    return pageName.substring(0, pageName.length - 5);
}

function setNewHeader(location){
 $(location).html('<div class="container"><h2 id="questionTitle" class="tt title_font page_title"></h2><div class="row"><div class="col-xs-12"><p class="text-center text-sty" id="statme"></p></div></div></div><div class="container"><div class="row"><div class="form-group"><div class="col-md-3 col-sm-3 col-xs-3 text-center"></div><div class="col-md-3 col-sm-3 col-xs-3 text-center"><h1 class="text-center text_color" id="word" ></h1></div><div class="col-md-3 col-sm-3 col-xs-3 text-center"><h1 class="text-center text_color" id="word_1" ></h1></div><div class="col-md-3 col-sm-3 col-xs-3 text-center"><h1 class="text-center text_color" id="word_2" ></h1></div></div></div></div>')
   $("#repeat_btn").bind("click",function(e){
      history.go(0);
   })
}
function setMainContainer(location){
   // setNewHeader(location);// temp place 
 $(location).append('<div id="main_container" class="container"></div><!--end container--><!--/////////////////////////////-->' )
}

function setMainContent(location){
   $(location).append('<div class="row"><div class="col-xs-12"><div class="contents"></div><!--end contents--></div><!--end col-xs-12--></div><!--end row-->')
}
function setParagraph(location){
   $(location).append('<div class="row"><div class="col-xs-12"><h2 class="title_font page_title"></h2><p class="page_desc text-justify"></p></div><!--end col-xs-12--></div><!--end row-->')
}
function setNotiMsg(location,text){
//   $('<div class="notification_msg wow rubberBand"><a href="javascript:void(0)" class="close_msg"><i class="fa fa-close"></i></a><p id="noti_msg">'+text+'</p></div><!--end notification_msg-->').appendTo(location).children(".close_msg").bind("click",function(e){
//       		$(".notification_msg").remove();
//	   })
   
}
function setFinalDeclar(afterLocation){
    $(afterLocation).after('<!--=========Modal=========--><div class="modal fade bs-example-modal-sm" id="card_modal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h4 class="modal-title" id="myModalLabel">تأكيد</h4></div><div class="modal-body"><div id="final-alert" class="alert alert-success"> تمت جميع الإختيارات بنجاح   </div></div></div></div></div><!--/////////////////////////////-->')
}
function setImgDragROW(location){
   $(location).append('<div class="row"><div class="img_drag"><div id="cardPile"></div><div id="cardSlots" ></div><div class="clearfix"></div></div><!--end fliping_content--></div><!--end row-->')
}

function playAudio(path,fName){
    $('<audio id="audioDemo" controls preload="auto"><source src="'+path+'/'+fName+'.mp3" type="audio/mp3"></audio>')[0].play()

}
function playAudio(pathWithOutExtention){
    $('<audio id="audioDemo" controls preload="auto"><source src="'+pathWithOutExtention+'.mp3" type="audio/mp3"></audio>')[0].play()

}
function playTapAudio(pathWithOutExtention){
    if(pathWithOutExtention!=""){
    audioTap=$('<audio id="audioDemo" controls preload="auto"><source src="'+pathWithOutExtention+'.mp3" type="audio/mp3"></audio>')
    audioTap[0].play()}

}
function setMedia(text,mediaData){
    //console.log(mediaData)
    
    var  media="";
    if(mediaData != undefined){
    if( mediaData.img != undefined  && mediaData.img != ""){
        media='<div class="media_img text-center all-media"  ><img src="'+mediaData.img+'"  /></div>'
    }else if( mediaData.video != undefined  &&  mediaData.video!= "" ){
        media='<div class="media_video text-center all-media" ><video width="100%" controls preload="no" poster="'+mediaData.video+'.png">                         <source type="video/mp4" src="'+mediaData.video+'.mp4"></source>                           <source type="video/ogg" src="'+mediaData.video+'.ogv"></source>                           <source type="video/webm" src="'+mediaData.video+'.webm"></source>Your browser does not support HTML5 video.</video></div>'
    }else if( mediaData.slider != undefined   &&  mediaData.slider != "" ){
        sNum=1;//slider number for different id 
        var olTemp="", imgTemp="",className="active";
        $.each(mediaData.slider,function(index,value){
            olTemp+='<li class="'+className+'" data-target="#carousel'+sNum+'" data-slide-to="'+index+'"></li>';
            imgTemp+='<div class="item '+className+' text-center all-media"><img src="'+value+'"></div>';
            className=""
        })
        media='<div class="media_slider " ><div id="carousel'+sNum+'" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators">'+olTemp+'</ol><div class="carousel-inner" role="listbox">'+imgTemp+'</div><a class="left carousel-control" href="#carousel'+sNum+'" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel'+sNum+'" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div><!---end of carousel---></div>'
        sNum++
    } 
         }
    //class="media_and_desc text-justify just_desc just_media  "
    if(text!=undefined && text!="" && media !=""){
        media='<div class="col-xs-6 media_and_desc"><div class="media_desc text-justify " >' + text + '</div></div><div class="col-xs-6 media_and_desc">' + media + '</div><div class="clearfix"> </div>'
    }else if(text!=undefined && text!="" && media ==""){
        media= '<div class="col-xs-12  text-justify just_desc"><div class="media_desc" >' + text + '</div><div class="clearfix"> </div></div><div class="clearfix"> </div>'
    }else{
        media='<div class="col-xs-10  col-xs-offset-1 just_media">' + media + '</div><div class="clearfix"> </div>'
    }
    media='<div class="row custom_media">'+media+'</div><!--  end media row  -->'
    return media
   
}  
  
 
 


