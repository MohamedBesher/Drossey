         var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
var sound_click ='../sound/click';

var flag=0;
var narration;
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

$(document).ready(function() { 
    // this key code to link with player and load json 
     /*   loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/Interactive_Images.js', function (a, b) {}).onload = function () {*/
  
  loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
      start();
        };
   // }
//end key code 
});

function start() {
    //page header
    if(mydata.staticData.pageHeader != undefined){
        $('#lesson_title').html(mydata.staticData.pageHeader);
    }else{
        $('.alhaytham_head').css('display','none');
    }
    //prompt
    if(mydata.staticData.prompt != undefined){
        $('.notification_msg p').html(mydata.staticData.prompt);
    }else{
        $('.notification_msg').css('display','none');
    }
    //page sub-title
    if(mydata.staticData.inside_title !='' && mydata.staticData.inside_title !=undefined){
        $('.ttitle').prepend('<p class="page_desc text-justify">'+mydata.staticData.inside_title+'</p>');
    }
    //page title
    if(mydata.staticData.title !='' && mydata.staticData.title != undefined){
        $('.ttitle').prepend('<h2 class="tt title_font page_title">'+mydata.staticData.title+'</h2>');
    } 
    
    $('.close_msg').click(function(){
        $('.notification_msg').remove();        
    });
//     $("#repeat_btn").bind("click",function(e){
//      history.go(0);
//   });
    loadLinks();
    
    //narration on page start
    if(mydata.staticData.narration != undefined && mydata.staticData.narration !=''){
        narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=mydata.staticData.narration+'.mp3';
        narration.load();
        narration.play();        
    }
    
    
    $('.image_thumb a img').click(function(){
		$(this).parent().parent().children('.img_desc').toggleClass('opend');
        
        //play sound effect
        var snd=document.getElementById('noise');
        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
        snd.src=sound_click+'.mp3';
        snd.load();
        snd.play();
        
        if($(this).parent().parent().children('.img_desc').hasClass('opend')){
            //play tab narration
            if(mydata.dynamicData[$(this).attr('id')].sub_narration != undefined && mydata.dynamicData[$(this).attr('id')].sub_narration != ''){
                narration=document.getElementById('narration');
                canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
                narration.src=mydata.dynamicData[$(this).attr('id')].sub_narration+'.mp3';
                narration.load();
                narration.play();
            }else{
                narration.pause();
            }
        }else{
            narration.pause();
        }        
	});    
}

function loadLinks(){
    if(mydata.dynamicData.length==1){
        loadOne();
    }else if(mydata.dynamicData.length==2){
        loadTwo();
    }else if(mydata.dynamicData.length%3==0 || mydata.dynamicData.length==5){
        loadThrees();
    }else if(mydata.dynamicData.length%4==0 || mydata.dynamicData.length%4==2 || mydata.dynamicData.length%4==3){
        loadFours();
    }else{
        loadThrees();
    }
}

function loadOne(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        $('.contents .row').append('<div class="col-xs-12"><div class="image_thumb wow zoomIn"><a href="javascript:void(0)"><img style="'+mydata.style+'" id="'+x1+'" class="img-responsive center-block" src="'+mydata.dynamicData[x1].image+'"/></a><div class="img_desc"><p class="title_font">'+mydata.dynamicData[x1].text+'</p></div></div></div>');
    }    
}

function loadTwo(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        $('.contents .row').append('<div class="col-xs-12 col-sm-6"><div class="image_thumb wow zoomIn"><a href="javascript:void(0)"><img style="'+mydata.style+'" id="'+x1+'"  class="img-responsive center-block" src="'+mydata.dynamicData[x1].image+'"/></a><div class="img_desc"><p class="title_font">'+mydata.dynamicData[x1].text+'</p></div></div></div>');
    }
}

function loadThrees(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        flag++;
        if (flag==4){
            flag=1;
            $('.contents .row').append('<div class="clearfix"></div>');
        }
        $('.contents .row').append('<div class="col-xs-12 col-md-4 col-sm-4"><div class="image_thumb wow zoomIn"><a href="javascript:void(0)"><img style="'+mydata.style+'" id="'+x1+'" class="img-responsive center-block" src="'+mydata.dynamicData[x1].image+'"/></a><div class="img_desc"><p class="title_font">'+mydata.dynamicData[x1].text+'</p></div></div></div>');
    }
}

function loadFours(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        flag++;
        if(flag==5){
            flag=1;
            $('.contents .row').append('<div class="clearfix"></div>');
        }
        $('.contents .row').append('<div class="col-xs-12 col-md-3 col-sm-3"><div class="image_thumb wow zoomIn"><a href="javascript:void(0)"><img  style="'+mydata.style+'" id="'+x1+'" src="'+mydata.dynamicData[x1].image+'"/></a><div class="img_desc"><p class="title_font">'+mydata.dynamicData[x1].text+'</p></div></div></div>');
    }
}