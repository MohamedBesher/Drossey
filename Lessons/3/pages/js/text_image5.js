var sound_click ='sounds/click';

var narration;
$(document).ready(function() {  
    // this key code to link with player and load json 
/*        loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/text_image5.js', function (a, b) {}).onload = function () {*/
        var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            start();
        };
//    }
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
//    $("#repeat_btn").bind("click",function(e){
//        history.go(0);
//    });
    $('.close_msg').click(function(){
        $('.notification_msg').remove();        
    }); 
    
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
    
    $('.text h3').html(mydata.dynamicData.head);
    
    for(var s=0; s<mydata.dynamicData.items.length;s++){
        $('.text ol').append('<p>'+mydata.dynamicData.items[s]+'</p>');
    }
    
    $('.ttitle div img').attr('src',mydata.dynamicData.image_source);
//    
    
    //narration on page start
//    if(mydata.staticData.narration != undefined && mydata.staticData.narration !=''){
//        narration=document.getElementById('narration');
//        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
//        narration.src=mydata.staticData.narration+'.mp3';
//        narration.load();
//        narration.play();
//        
//        $('#narration').on('ended', function() {
//            play_firstTab_audio();
//        });
//    }else{
//        play_firstTab_audio();
//    }
}
