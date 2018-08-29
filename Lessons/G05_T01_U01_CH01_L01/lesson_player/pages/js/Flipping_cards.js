  var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
//var sound_click ='../sound/click';

var narration;
narration=document.getElementById('narration');

$(document).ready(function() {
    // this key code to link with player and load json 
     /*   loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/Flipping_cards.js', function (a, b) {}).onload = function () {*/

   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            start();
        };
   // }
//end key code 
});

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
		$(".notification_msg").remove();
	});
    $("#repeat_btn").bind("click",function(e){
        history.go(0);
    });
    
    //narration on page start
    if(mydata.staticData.narration != undefined && mydata.staticData.narration !=''){
        narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=canPlayMP3?mydata.staticData.narration+'.ogg':mydata.staticData.narration+'.mp3';
        narration.load();
        narration.play();
    }
    
    loadLinks();
    
}
function loadLinks(){    
    //active data
    if(mydata.dynamicData.activeData.length != 0){
        $('.item1').addClass('active');
        for(var x1=0;x1<mydata.dynamicData.activeData.length;x1++){
            if(mydata.dynamicData.activeData.length==1){
                $('.item1').append('<div class="col-xs-12 col-md-12 col-sm-12"><div class="box_text" id="active'+x1+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>');
                
                if(mydata.dynamicData.activeData[x1].frontPic != ''){
                    $('#active'+x1+' .front_box').prepend('<img src="'+mydata.dynamicData.activeData[x1].frontPic+'"/>');
                }
                
            }else if(mydata.dynamicData.activeData.length==2){
                $('.item1').append('<div class="col-xs-6 col-md-6 col-sm-6"><div class="box_text" id="active'+x1+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>');
                
                if(mydata.dynamicData.activeData[x1].frontPic != ''){
                    $('#active'+x1+' .front_box').prepend('<img src="'+mydata.dynamicData.activeData[x1].frontPic+'"/>');
                }
                
            }else if(mydata.dynamicData.activeData.length==3 || mydata.dynamicData.activeData.length==5 || mydata.dynamicData.activeData.length==6){
                $('.item1').append('<div class="col-xs-4 col-md-4 col-sm-4"><div class="box_text" id="active'+x1+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>');
                
                if(mydata.dynamicData.activeData[x1].frontPic != ''){
                    $('#active'+x1+' .front_box').prepend('<img src="'+mydata.dynamicData.activeData[x1].frontPic+'"/>');
                }
                
            }else{
                $('.item1').append('<div class="col-xs-3 col-md-3 col-sm-3"><div class="box_text" id="active'+x1+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>');
                
                if(mydata.dynamicData.activeData[x1].frontPic != ''){
                    $('#active'+x1+' .front_box').prepend('<img src="'+mydata.dynamicData.activeData[x1].frontPic+'"/>');
                }
            } 
            
            
            $('#active'+x1+' .front_box p').html(mydata.dynamicData.activeData[x1].title);
            $('#active'+x1+' .back_box p').html(mydata.dynamicData.activeData[x1].paragraph);
        }            
        
    }else{
        $('.item1').remove();
        $('.hotspot_slider_control').remove();
        $('.item2').addClass('active');
    }
    
    //other data
    if(mydata.dynamicData.otherData.length != 0){        
        for(var x2=0;x2<mydata.dynamicData.otherData.length;x2++){
            if(mydata.dynamicData.otherData.length==1){
                $('.item2').append('<div class="col-xs-12 col-md-12 col-sm-12"><div class="box_text" id="other'+x2+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>'); 
                
                if(mydata.dynamicData.otherData[x2].frontPic !=''){
                    $('#other'+x2+' .front_box').prepend('<img src="'+mydata.dynamicData.otherData[x2].frontPic+'"/>');
                }
                       
            }else if(mydata.dynamicData.otherData.length==2){
                $('.item2').append('<div class="col-xs-6 col-md-6 col-sm-6"><div class="box_text" id="other'+x2+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>');
                
                if(mydata.dynamicData.otherData[x2].frontPic !=''){
                    $('#other'+x2+' .front_box').prepend('<img src="'+mydata.dynamicData.otherData[x2].frontPic+'"/>');
                }
                
            }else if(mydata.dynamicData.otherData.length==3 || mydata.dynamicData.otherData.length==5 || mydata.dynamicData.otherData.length==6){
                $('.item2').append('<div class="col-xs-4 col-md-4 col-sm-4"><div class="box_text" id="other'+x2+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>');
                
                if(mydata.dynamicData.otherData[x2].frontPic !=''){
                    $('#other'+x2+' .front_box').prepend('<img src="'+mydata.dynamicData.otherData[x2].frontPic+'"/>');
                }
                
            }else{
                $('.item2').append('<div class="col-xs-3 col-md-3 col-sm-3"><div class="box_text" id="other'+x2+'"><div class="front_box"><p></p></div><div class="back_box"><p></p></div></div></div>');
                
                if(mydata.dynamicData.otherData[x2].frontPic !=''){
                    $('#other'+x2+' .front_box').prepend('<img src="'+mydata.dynamicData.otherData[x2].frontPic+'"/>');
                }
                
            }        
            $('#other'+x2+' .front_box p').html(mydata.dynamicData.otherData[x2].title);
            $('#other'+x2+' .back_box p').html(mydata.dynamicData.otherData[x2].paragraph);
        }
    }else{
        $('.item2').remove();
        $('.hotspot_slider_control').remove();
    }
    
    //on mouse over on box
    $('.box_text').mouseover(function(){        
        //play sound effect
       var snd=document.getElementById('noise');
//        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
//        snd.src=canPlayMP3?sound_click+'.ogg':sound_click+'.mp3';
//        snd.load();
//        snd.play();
                
        //play tab narration
        if($(this).parent().parent().hasClass('item1')){
            if(mydata.dynamicData.activeData[$(this).parent().index()].sub_narration != undefined && mydata.dynamicData.activeData[$(this).parent().index()].sub_narration != ''){
//                narration=document.getElementById('narration');
                canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
                narration.src=canPlayMP3?mydata.dynamicData.activeData[$(this).parent().index()].sub_narration+'.ogg':mydata.dynamicData.activeData[$(this).parent().index()].sub_narration+'.mp3';
                narration.load();
                narration.play();
            }else{
//                narration.pause();
            }
        }else if($(this).parent().parent().hasClass('item2')){
            if(mydata.dynamicData.otherData[$(this).parent().index()].sub_narration != undefined && mydata.dynamicData.otherData[$(this).parent().index()].sub_narration != ''){
//                narration=document.getElementById('narration');
                canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
                narration.src=canPlayMP3?mydata.dynamicData.otherData[$(this).parent().index()].sub_narration+'.ogg':mydata.dynamicData.otherData[$(this).parent().index()].sub_narration+'.mp3';
                narration.load();
                narration.play();
            }else{
//                narration.pause();
            }
        }        
    });
    
    //on mouse out off box
    $('.box_text').mouseout(function(){
        narration.pause();
    });    
}