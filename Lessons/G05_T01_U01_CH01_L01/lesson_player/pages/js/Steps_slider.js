 var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
var sound_click ='../sound/click';

var flag=0;
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
        loadScript('../Player/lessons/' + lessonID + '/media/Steps_slider.js', function (a, b) {}).onload = function () {*/
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
   
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
        $('.ttitle').prepend('<p class="page_desc text-justify">'+mydata.staticData.inside_title+'</p>')
    }
    //page title
    if(mydata.staticData.title !='' && mydata.staticData.title != undefined){
        $('.ttitle').prepend('<h2 class="tt title_font page_title">'+mydata.staticData.title+'</h2>')
    }    
  
    $('.close_msg').click(function(){
        $('.notification_msg').remove();        
    });
    
    //stop sliding
    $('.carousel').carousel({
        interval: false
    });
    
    //play first narration on first load
    if(mydata.staticData.narration != undefined){
            narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=canPlayMP3?mydata.staticData.narration+'.ogg':mydata.staticData.narration+'.mp3';
        narration.load();
        narration.play();
        
        $('#narration').on('ended', function() {
            play_firstTab_audio();
        });
        
    }else{
            play_firstTab_audio();
    }
    
    loadLinks();
}

function loadLinks(){        
    for(var x=0;x<mydata.dynamicData.length;x++){
        var d=x+1;
        $('.carousel-inner').append('<div class="item"><img src="'+mydata.dynamicData[x].image+'"><article><p>'+mydata.dynamicData[x].paragraph+'</p></article></div>');
        $('.btns').append('<span>'+d+'</span>');
    }
    $('.item').first().addClass('active');
    $('.btns span:nth-child(1)').addClass('active');
    
    $('.btns span').click(function(){
        if($('.btns span').hasClass('active')){
            $('.btns span').removeClass('active');
        }
        $(this).addClass('active');
        $('#carousel').carousel($(this).index())
    });

    //on slider change
    $('#carousel').bind('slide.bs.carousel', function (e) {
        //play sound effect
        var snd=document.getElementById('noise');
        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
        snd.src=canPlayMP3?sound_click+'.ogg':sound_click+'.mp3';
        snd.load();
        snd.play();
        
        var slideTo = $(e.relatedTarget).index();
        //play tab narration
        if(mydata.dynamicData[slideTo].sub_narration != undefined && mydata.dynamicData[slideTo].sub_narration != ''){
            narration=document.getElementById('narration');
            canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
            narration.src=canPlayMP3?mydata.dynamicData[slideTo].sub_narration+'.ogg':mydata.dynamicData[slideTo].sub_narration+'.mp3';
            narration.load();
            narration.play();
            
            $('#narration').on('ended', function() {                
                $('#carousel').carousel('next')
            });
            
        }else{
            narration.pause();
        }
        
        if($('.btns span').hasClass('active')){
            $('.btns span').removeClass('active');
        }
        $('.btns span:eq('+slideTo+')').addClass('active');
    });
}

function play_firstTab_audio(){
      if(mydata.dynamicData[0].sub_narration != undefined && mydata.dynamicData[0].sub_narration != ''){
    narration=document.getElementById('narration');
    canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
    narration.src=canPlayMP3?mydata.dynamicData[0].sub_narration+'.ogg':mydata.dynamicData[0].sub_narration+'.mp3';
    narration.load();
    narration.play();
    
    $('#narration').on('ended', function() {                
        $('#carousel').carousel('next')
    });
      }
    else{
          narration.pause();
    }
}