var correctAnswers = 0;
var queCount;
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
    // this key code to link with player and load json 
       /* loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
      //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init(pageNum)
        };
    //}
//end key code 
    
    
    }
 
function init(pageNum) {

    loadHTMLTags(pageNum);
 // Create the pile of shuffled cards
  var answers = [ ];
//console.log(pages[pageNum].data_gallary[0].co_answer)
    // define dg as data array for gallary from json 
    var DG=pages[pageNum].data_quiz;
    queCount=DG.length;
 // Create the card slots
	
    var answers=[];
    //this variable to save data wrong answer array
  for ( var i=0; i<DG.length; i++ ) {
        answers=[];
    var DWA=DG[i].wro_answer;
//      console.log(DWA.length)
   answers.push({x:0, y:DG[i].co_answer});
    for (var j=0;j<DWA.length;j++){
          answers.push({x:j+1, y:DWA[j].wrong});
    }  
  answers.sort( function() { return Math.random() - .5 } );
      
      var mySelect = '#Q'+DG[i].qu_num;
      //this code to add options to select
      var seloption = "";
$.each(answers,function(i){
    
    seloption += '<option value="'+answers[i].x+'">'+answers[i].y+'</option>'; 
});
//end

 // set select
$('<div class="col-md-3 col-sm-3 col-xs-4"><div class="form-group"><label  style="font-size:18px" for="'+mySelect+'">'+DG[i].question+'</label><select class="form-control" id="'+mySelect+'">'+seloption+'</select></div></div><!--end col-md-3 col-sm-3 col-xs-12-->' ).appendTo( '#main_content' );
//      console.log(DG[i].question)
      
  }
    // check answers
    
 $('#btn-sumit').click(function(){
     correctAnswers = 0;
     for(var i=0; i<DG.length; i++){
//        console.log('DG[i].question') 
var currAnswer=$('select[id=#Q'+DG[i].qu_num+']').find(":selected").val();
         if (currAnswer==0){
             correctAnswers++;
$('select[id=#Q'+DG[i].qu_num+']').removeClass('wrong-answer').addClass('correct-answer')
         }else{
            $('select[id=#Q'+DG[i].qu_num+']').removeClass('correct-answer').addClass('wrong-answer')
         }

     }
     
if (correctAnswers<queCount*.5)
     {
         $('#final-alert').removeClass('alert-success').addClass('alert-danger')
         playTapAudio(wAnswerSoundPath)
     }else{
          $('#final-alert').removeClass('alert-danger').addClass('alert-success') 
          playTapAudio(cAnswerSoundPath)
     }
     $('#final-alert').text('حصلت علي '+correctAnswers+' من '+queCount+'').css('text-align', 'center');
      $('#card_modal').modal('show');
     
  // $('#btn-sumit').prop( "disabled", true );
 })
}
 
function loadHTMLTags(pageNum) {
    $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    setMainContent('#main_container'); 
    $(".contents").addClass('temp_17')
    if(pages[pageNum].article!=undefined){
        setParagraph(".contents")
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);}else{$('.page_title').remove()}
        $('.page_desc').html(pages[pageNum].article.text);
        setContent(".page_desc");
        //console.log(pages[pageNum].article.text.length)
    }else{
         setContent(".contents");
    }
    
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration)}    
    }   
    setFinalDeclar("#main_container");
    
}

function setContent(afterLocation) {
    $(afterLocation).after('<div id="main_content"></div><div class="clearfix"></div><div class="col-md-4 col-md-offset-4 col-sm-4 col-sm-offset-4 col-xs-12"><button id="btn-sumit" type="button" class="btn btn-primary btn-lg btn-block">تحقق الأن</button><br/></div><!--end col-xs-12-->')
     
  
     
}

