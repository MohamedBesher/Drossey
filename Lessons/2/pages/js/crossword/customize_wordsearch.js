var wordsDetails=[],example=false,report=false;
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
function start(pageNum) {
   // console.log("text ")
    // this key code to link with player and load json 
     /*   loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
             //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            loadHTMLTags(pageNum);
        };
    //}
//end key code 
    
    
    }
function loadHTMLTags(pageNum){
     console.log(pages[pageNum].words[0].definition.length);
    $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    setMainContent('#main_container'); 
    $(".contents").addClass('temp_crossword');
    if(pages[pageNum].article!=undefined){
        setParagraph(".contents");
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);}else{$('.page_title').remove();}
        $('.page_desc').text(pages[pageNum].article.text);
    }
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined && pages[pageNum].narration!=""){
        playTapAudio(pages[pageNum].narration);
    }
    if(pages[pageNum].example!=undefined){
        if(pages[pageNum].example){example=true;}
    }
    if(pages[pageNum].report!=undefined){
        if(pages[pageNum].report.enabled){
            $('body').append('<!-- Creates the bootstrap modal where the image will appear --><div class="modal fade" id="reportModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">  <div class="modal-dialog">    <div class="modal-content">      <div class="modal-header">        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>        <h4 class="modal-title" id="myModalLabel">hgj</h4>      </div>      <div id="preview_location" class="modal-body">              </div>      <div class="modal-footer">        <button type="button" class="btn btn-default close_report" data-dismiss="modal">Close</button>      </div>    </div>  </div></div>');
            $('#reportModal #myModalLabel').html(pages[pageNum].report.header);
            $('#reportModal .close_report').html(pages[pageNum].report.footer);
            report=true;
            
        }
    }
    setCrosswordRow(".contents");
    
    //setFinalDeclar('#main_container');
    
     
    crossWordInit(pageNum);
}
function crossWordInit(pageNum){
   
     //      var words = "earth,mars,mercury,neptune,pluto";
    wordsDetails=pages[pageNum].words;
   /* var ahmed='  hd hjhk ';
    console.log("a"+$.trim(ahmed)+"a")*/
    var words="";var avalSize=0;
    for(var i=0; i<wordsDetails.length;i++){
    if(i != wordsDetails.length-1)
    {
        words+=$.trim(wordsDetails[i].word)+',';
        avalSize=getLongest(avalSize,$.trim(wordsDetails[i].word).length);
    }
    else
    {
    words+=$.trim(wordsDetails[i].word);
        avalSize=getLongest(avalSize,$.trim(wordsDetails[i].word).length);
    }
    }
    
    
//     console.log(avalSize)
     if (pages[pageNum].size>=avalSize){
         avalSize=pages[pageNum].size;
     }
   
    //attach the game to a div
    $("#theGrid").wordsearchwidget
    (
        {
            "wordlist" : words,
            "gridsize" : avalSize,
            "onWordFound" : function(ob) {

                if(ob.feed_back!=undefined && ob.feed_back!=""){
//                    feedBackMsg('#onWordFound','<span>  </span>'+ ob.feed_back  )
                    d=setTimeout(function(){
                        clearTimeout(d);
                        $('.feed_back').remove();
                    },3000);
                }
                if(ob.narration!=undefined && ob.narration!=""){
                        //support sound
                        if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause();}
                        playTapAudio(ob.narration);
                }
                
                
            },
            "onWordSearchComplete" :function(){
                if(pages[pageNum].final_feed_back!=undefined && pages[pageNum].final_feed_back!=""){
                    //$('#onWordSearchComplete').html('<p>'+pages[pageNum].final_feed_back+'</p>');
                    feedBackMsg('#onWordSearchComplete',pages[pageNum].final_feed_back );
                    d=setTimeout(function(){
                        clearTimeout(d);
                        $('.feed_back').remove();
                    },3000);
                }
                 if( report ){
                     var htmlReportTags='<div class="row">';
                     $.each(wordsDetails,function(index,value){
                         htmlReportTags+='<div class="col-xs-12"> ';
                         htmlReportTags+='<span class="text-info"> '+value.word+' </span> '+value.definition;
                         htmlReportTags+='</div>';
                     });
                     htmlReportTags+='</div>';
                    $('#preview_location').html(htmlReportTags);
                     jQuery.noConflict(); 
                     $('#reportModal').modal('show'); 
                 }
                
            }
        }
    );
     if (avalSize <= 6 ){
       $("#theGrid").addClass('size_x');
        $(".contents").addClass('size_x');
       $('#rf-wordcontainer').addClass('col-xs-12').addClass('col-md-6').addClass('col-sm-12') ;
    }else if(avalSize <= 8){
        $("#theGrid").addClass('size_xl') ;
        $(".contents").addClass('size_xl');
        $('#rf-wordcontainer').addClass('col-xs-12').addClass('col-md-6').addClass('col-sm-12'); 
    }else if(avalSize > 10){
       $("#theGrid").addClass('size_3xl');
       $(".contents").addClass('size_3xl');
       $('#rf-wordcontainer').addClass('col-xs-12').addClass('col-md-6').addClass('col-sm-12'); 
    }else {
       $("#theGrid").addClass('size_2xl'); 
       $(".contents").addClass('size_2xl');
       $('#rf-wordcontainer').addClass('col-xs-12').addClass('col-md-6').addClass('col-sm-12') ;
    }
    
  
}
function getLongest(a,b){
    if(a>b){return a}else{return b};
}
function setCrosswordRow(location){
    $(location).append(' <!--crossword starting--><div id="theGrid"></div><div class="clearfix"></div><div id="onWordSearchComplete"></div><div id="onWordFound" ></div><!--end crossword--><div class="clearfix"></div><!--crossword ending --> ');
}
function feedBackMsg(location,text){
   $('<div class="notification_msg feed_back wow rubberBand"><a href="javascript:void(0)" class="close_msg"><i class="fa fa-close"></i></a><p id="noti_msg">'+text+'</p></div><!--end notification_msg-->').appendTo(location).children(".close_msg").bind("click",function(e){
       		$(".notification_msg").remove();
           

	   });
   
}