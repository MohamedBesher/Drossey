var jsonFile = sessionStorage.getItem('jsonFile');
var lesson_id=  sessionStorage.getItem('lessonId');
var sound_click ='../sound/click';
var narration;
var video_number=0;
$(document).ready(function() {
    // this key code to link with player and load json 
    /*    loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/Tabs_Up.js', function (a, b) {}).onload = function () {*/
    
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            start();
        };
 //   }
//end key code         
})
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

function start() {  
    //page title in address bar
    if(mydata.staticData.pageName != undefined){
        document.title =mydata.staticData.pageName;
    }else{
        document.title = '.';
    }
    
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
        $('.ttitle').prepend('<p class="page_desc text-justify">'+mydata.staticData.inside_title+'</p>')
    }
    //page title
    if(mydata.staticData.title !='' && mydata.staticData.title != undefined){
        $('.ttitle').prepend('<h2 class="tt title_font page_title">'+mydata.staticData.title+'</h2>')
    }
    
    //narration on page start
    if(mydata.staticData.narration != undefined && mydata.staticData.narration !=''){
        narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=mydata.staticData.narration+'.mp3';
        narration.load();
        narration.play();
        
        $('#narration').on('ended', function() {
            play_firstTab_audio();
        });
    }else{
        play_firstTab_audio();
    }    
    
//    $("#repeat_btn").bind("click",function(e){
//        history.go(0);
//    });
//    
    $('.close_msg').click(function(){
        $('.notification_msg').remove();        
    });
    
    loadTabs();
}

function loadTabs(){
    for(var n=0; n<mydata.dynamicData.length; n++){
        if(mydata.dynamicData.length==1){
            $( ".nav-tabs" ).append('<div class="col-xs-12 col-md-12 col-sm-12"><a data-toggle="tab" href="#" id="'+n+'" class="lesson_link shadow_eff wow bounceIn " data-wow-delay=".3s">'+mydata.dynamicData[n].title+'</a></div>');
        }else if(mydata.dynamicData.length==2){
            $( ".nav-tabs" ).append('<div class="col-xs-6 col-md-6 col-sm-6"><a data-toggle="tab" href="#" id="'+n+'" class="lesson_link shadow_eff wow bounceIn " data-wow-delay=".3s">'+mydata.dynamicData[n].title+'</a></div>');
        }else if(mydata.dynamicData.length==3){
            $( ".nav-tabs" ).append('<div class="col-xs-4 col-md-4 col-sm-4"><a data-toggle="tab" href="#" id="'+n+'" class="lesson_link shadow_eff wow bounceIn " data-wow-delay=".3s">'+mydata.dynamicData[n].title+'</a></div>');
        }else if(mydata.dynamicData.length==4){
            $( ".nav-tabs" ).append('<div class="col-xs-3 col-md-3 col-sm-3"><a data-toggle="tab" href="#" id="'+n+'" class="lesson_link shadow_eff wow bounceIn " data-wow-delay=".3s">'+mydata.dynamicData[n].title+'</a></div>');
        }else{
            $( ".nav-tabs" ).append('<div class="col-xs-2 col-md-2 col-sm-2"><a data-toggle="tab" href="#" id="'+n+'" class="lesson_link shadow_eff wow bounceIn " data-wow-delay=".3s">'+mydata.dynamicData[n].title+'</a></div>');
        }
                

      /*  $('.tab-content ').append('<div id="'+n+'tab" class="tab-pane fade in"><div class="col-xs-12"><div class="contents"><div class="row" id="allcontents'+n+'"></div></div></div></div>');*/
    }
    
    $('.tab-content ').append('<div><div class="col-xs-12"><div class="contents"><div class="row" id="allcontents"></div></div></div></div>');


    
   $('a.lesson_link').first().addClass('active');
     loadTabContent(0);

    $('.lesson_link').click(function(){

        if($('a.lesson_link').hasClass('active')){
            $('a.lesson_link').removeClass('active');
        }
        $(this).addClass('active');
        var textLength = $(this).text().length;
       $('a:hover:before').css("top","10px");
        //play sound effect
        var snd=document.getElementById('noise');
        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
        snd.src=sound_click+'.mp3';
        snd.load();
        snd.play();        
        
        //play tab narration
        if(mydata.dynamicData[$(this).parent().index()].sub_narration != undefined && mydata.dynamicData[$(this).parent().index()].sub_narration != ''){
            narration=document.getElementById('narration');
            canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
            narration.src=mydata.dynamicData[$(this).parent().index()].sub_narration+'.mp3';
            narration.load();
            narration.play();
            $('#narration').on('ended', function() {
                    narration.pause();
            });
        }else{
            narration.pause();
        }
        
        //condition to play video on open textWithVideo tab
        $('video').each(function() {
            $(this).get(0).pause();
        });

        
       $('.lesson_link').css('background-color','white');
        $('.lesson_link').css('color','#149FD3');
        $('.lesson_link').css('border-bottom','none');
        
        $(this).css('background-color','#149FD3');
        $(this).css('border-bottom','2px solid red');        
        $(this).css('color','#FFF');
        
        loadTabContent($(this).attr("id"));        
        
    });    
    
  $('.nav-tabs div:nth-child(1) a').css('background-color','#149FD3');
    $('.nav-tabs div:nth-child(1) a').css('border-bottom','2px solid red');
    $('.nav-tabs div:nth-child(1) a').css('color','#FFF');
    $('#0tab').addClass('active');
    
    loadTabContent();
  
}
var id=[];

function loadTabContent(newn){

        
        //text onlt
        if(mydata.dynamicData[newn].type=='text'){
             $(id).each(function(i,v){
                clearTimeout(v);
            });
            
            $('#allcontents').html('<div class="col-xs-12 col-md-12 col-sm-12"><div class="contents"><article></article></div></div>');
           
            $(mydata.dynamicData[newn].paragraph).each(function(index,value){
             
                 id.push(setTimeout(function(){  $('#allcontents .contents  article ').append(value.text);},value.time));
                
            });
        
       
//            $('.contents article p').css('-webkit-column-count','2');
//            $('.contents article p').css('-moz-column-count','2');
//            $('.contents article p').css('column-count','2');
//            $('.contents article p').css('-webkit-column-gap','40px');
//            $('.contents article p').css('-moz-column-gap','40px');
//            $('.contents article p').css('column-gap','40px');
         
        //image only
        }else if(mydata.dynamicData[newn].type=='image'){
             $(id).each(function(i,v){
                clearTimeout(v);
            });
             id.push(setTimeout(function(){  $('#allcontents').html('<div class="col-xs-12 col-md-12 col-sm-12"><div class="contents"><img src="'+mydata.dynamicData[newn].mediaSource.src+'" class="animated zoomIn"/></div</div>');}, mydata.dynamicData[newn].mediaSource.time));
            
        //video only
        }else if(mydata.dynamicData[newn].type=='video'){
            
              $(id).each(function(i,v){
                clearTimeout(v);
            });
            $('#allcontents').html('<div class="col-xs-12 col-md-12 col-sm-12"><div class="contents"><video id="'+video_number+'" controls style="width:100%"><source src="'+mydata.dynamicData[newn].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[newn].mediaSource+'.mp4" type="video/mp4"/></video></div></div>');
        
            video_number++;
            
        //slider only
        }else if(mydata.dynamicData[newn].type=='slider'){
                $(id).each(function(i,v){
                clearTimeout(v);
            });
            
            $('#allcontents').html('<div class="col-xs-12 col-md-12 col-sm-12"><div class="contents"><div class="media_cont" id="media_cont'+newn+'"></div></div></div>');
            
            loadSlider(newn);
        
        //text with image
        }else if(mydata.dynamicData[newn].type=='textWithImage'){
             //clear all time out
           $(id).each(function(i,v){
                clearTimeout(v);
            });
            $('#allcontents').html('<div id="articalContent" class="col-xs-12 col-md-8 col-sm-6"><article></article></div>');
            $('#allcontents').append('<div id="imgContent" class="col-xs-12 col-md-4 col-sm-6"></div>');
            $(mydata.dynamicData[newn].paragraph).each(function(index,value){
             
                 id.push(setTimeout(function(){ $('#articalContent  article').append(value.text);},value.time));
                
            });
        
        id.push(setTimeout(function(){ $('#imgContent').append('<img src="'+mydata.dynamicData[newn].mediaSource.src+'" class="animated zoomIn" style="'+mydata.dynamicData[newn].style+'" />');},mydata.dynamicData[newn].mediaSource.time));

            
            /*$('#allcontents').html('<div class="col-xs-6 col-md-6 col-sm-6"><article><p>'+mydata.dynamicData[newn].paragraph+'</p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><img src="'+mydata.dynamicData[newn].mediaSource+'"/></div>');*/
        
        //text with video
        }else if(mydata.dynamicData[newn].type=='textWithVideo'){
            
              $(id).each(function(i,v){
                clearTimeout(v);
            });
            $('#allcontents').html('<div id="articalContent" class="col-xs-12 col-md-6 col-sm-6"><article></article></div>');
            $('#allcontents').append('<div id="vidContent" class="col-xs-12 col-md-6 col-sm-6"></div>');
            $(mydata.dynamicData[newn].paragraph).each(function(index,value){
             
                 id.push(setTimeout(function(){ $('#articalContent  article').append(value.text);},value.time));
                
            });
          id.push(setTimeout(function(){ $('#allcontents #vidContent').append('<video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[newn].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[newn].mediaSource+'.mp4" type="video/mp4"/></video</div>');},mydata.dynamicData[newn].mediaSource.time)); 
            ;
            
            video_number++;
        
        //text with slider
        }else if(mydata.dynamicData[newn].type=='TextWithSlider'){
            
              $(id).each(function(i,v){
                clearTimeout(v);
            });
            
            $('#allcontents').html('<div id="articalContant" class="col-xs-12 col-md-6 col-sm-6"><article></article></div>');
            $('#allcontents').append('<div id="slidContent" class="col-xs-12 col-md-6 col-sm-6"></div>');
          $(mydata.dynamicData[newn].paragraph).each(function(index,value){
  
                 id.push(setTimeout(function(){ $('#allcontents #articalContant article').append(value.text);},value.time));
                
        });
        $('#allcontents #slidContent').append('<div class="media_cont"></div>');
            
            
            loadSlider(newn);           
        }
    
}

//to load slider on screen
function loadSlider(newn){
    $('.media_cont').append('<div id="carousel" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a class="left carousel-control" href="#carousel" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>');
            
    for(var nn=0;nn<mydata.dynamicData[newn].mediaSource.length;nn++){
        $('#carousel ol').append('<li data-target="#carousel" data-slide-to="'+nn+'"></li>');
        $('#carousel .carousel-inner').append('<div class="item"><img src="'+mydata.dynamicData[newn].mediaSource[nn]+'"  style="'+mydata.dynamicData[newn].style+'" ></div>');
    }
    
    $('#carousel li:nth-child(1), #carousel .carousel-inner div:nth-child(1)').addClass('active');    
}

function play_firstTab_audio(){
    narration=document.getElementById('narration');
    canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
    narration.src=mydata.dynamicData[0].sub_narration+'.mp3';
    narration.load();
    narration.play();
    $('#narration').on('ended', function() {
            narration.pause();
    });
}