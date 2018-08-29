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
     
//end key code 
    
    
    }
 
function init(pageNum) {

    loadHTMLTags(pageNum);
    
 // Create the pile of shuffled cards
  var answers = [ ];
  var Status = [ ];
  var Type=[ ];
//  var mydata = JSON.parse(words);
//    var name=[];
//console.log(pages[pageNum].data_gallary[0].co_answer)
    // define dg as data array for gallary from json 
    var DG=pages[pageNum].data_quiz;
//    var Words=pages[pageNum].words;
    queCount=DG.length;
 // Create the card slots
	var Status = [ ];
    var answers=[];
    var Type=[ ];

    
    
    //this variable to save data wrong answer array
  for ( var i=0; i<DG.length; i++ ) {
      Status = [ ];
      answers=[];
      Type=[ ];

    var DWA=DG[i].wro_answer;
    var DWAStatus=DG[i].wro_Status;
    var DWAType=DG[i].wro_type;

   answers.push({x:0, y:DG[i].co_answer});
    for (var j=0;j<DWA.length;j++){
          answers.push({x:j+1, y:DWA[j].wrong});
    }  
     Status.push({x:0, y:DG[i].status});
     Status.push({x:1, y:DWAStatus})
     
     Type.push({x:0, y:DG[i].type});
     Type.push({x:1, y:DWAType})

   answers.sort( function() { return Math.random() - .5 } );
   Status.sort( function() { return Math.random() - .5 } );
   Type.sort( function() { return Math.random() - .5 } );
      
      var mySelect = '#Q'+DG[i].qu_num;
      var mySelectStatus = '#Q'+DG[i].qu_num+1;
      var mySelectType = '#Q'+DG[i].qu_num+2
      
      //this code to add options to select
      var seloption = "";
      var statusSelection = "";
      var typeSelection = "";
      


$.each(answers,function(i){
    seloption += '<option value="'+answers[i].x+'">'+answers[i].y+'</option>'; 


    
});
      $.each(Status,function(i){
   
    statusSelection+= '<option value="'+Status[i].x+'">'+Status[i].y+'</option>'; 
  

    
});
      $.each(Type,function(i){

    typeSelection+= '<option value="'+Type[i].x+'">'+Type[i].y+'</option>'; 

    
});
//end

  
$('<div class="row"><div class="form-group"><div class="col-md-3 col-sm-3 col-xs-3 text-center"><label for="'+mySelect+'" >'+DG[i].question+'</label></div><div class="col-md-3 col-sm-3 col-xs-3 margin_select"><select class="form-control" id="'+mySelect+'">'+seloption+'</select> </div> <div class="col-md-3 col-sm-3 col-xs-3 margin_select"><select class="form-control" id="'+mySelectStatus+'">'+statusSelection+'</select> </div><div class="col-md-3 col-sm-3 col-xs-3 margin_select"><select class="form-control" id="'+mySelectType+'">'+typeSelection+'</select> </div></div>').appendTo('#main_content')      

  }
    //no of pages
    count = Object.keys(pages).length;
    if(count > 1)
        {
            for(var i = 0; i< count; i++)
            {
                if(i == 0)
                {
                    $('<li onclick="getprev()"><a href="#" aria-label="Previous"><span aria-hidden="true">&laquo; السابق</span></a></li><li onclick=paging('+i+') id='+i+' ><a href="#">1</a></li>').appendTo(".pagination");
                }
                else if(i == 1 || i == 2 || i == count-1)
                {
             
                    $('<li id='+i+' onclick=paging('+i+')><a href="#">'+Number(i+1)+'</a></li>').appendTo(".pagination");
                }
                else if(i == 3)
                {
                    $('<li id='+i+' onclick=paging('+i+')><a href="#">...</a></li>').appendTo(".pagination");
                }
                else if(i > 3)
                {
                    $('<li id='+i+' onclick=paging('+i+') class="shouldHide"><a href="#">...</a></li>').appendTo(".pagination");
                }
                if(i == count-1)
                {
             
                    $('<li onclick=getNext()><a href="#" aria-label="Next"><span aria-hidden="true">التالى &raquo;</span> </a></li>').appendTo(".pagination");;
                }
              
                
               
            }
            
        }
    $('.shouldHide').hide();
    $("#"+pageNum).addClass("active");
    
    
    // check answers
    
 $('#btn-sumit').click(function(){
     correctAnswers = 0;
     for(var i=0; i<DG.length; i++){
//        console.log('DG[i].question') 
         var currAnswer=$('select[id=#Q'+DG[i].qu_num+']').find(":selected").val();
         var currAnswerStatus=$('select[id=#Q'+DG[i].qu_num+1+']').find(":selected").val();
         var currAnswerType=$('select[id=#Q'+DG[i].qu_num+2+']').find(":selected").val();

         if (currAnswer==0){
             correctAnswers++;
$('select[id=#Q'+DG[i].qu_num+']').removeClass('wrong-answer').addClass('correct-answer')
         }else{
            $('select[id=#Q'+DG[i].qu_num+']').removeClass('correct-answer').addClass('wrong-answer')
         }
         //status
         
            if (currAnswerStatus==0){
             correctAnswers++;
$('select[id=#Q'+DG[i].qu_num+1+']').removeClass('wrong-answer').addClass('correct-answer')
         }else{
            $('select[id=#Q'+DG[i].qu_num+1+']').removeClass('correct-answer').addClass('wrong-answer')
         }
         
         //type
                     if (currAnswerType==0){
             correctAnswers++;
$('select[id=#Q'+DG[i].qu_num+2+']').removeClass('wrong-answer').addClass('correct-answer')
         }else{
            $('select[id=#Q'+DG[i].qu_num+2+']').removeClass('correct-answer').addClass('wrong-answer')
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
     $('#final-alert').text('حصلت علي '+correctAnswers+' من '+queCount*3+'').css('text-align', 'center');
      $('#card_modal').modal('show');
     
  // $('#btn-sumit').prop( "disabled", true );
 })
}
 
function loadHTMLTags(pageNum) {
    $('.title').text(pages[pageNum].name);

    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
        $('#statme').html(pages[pageNum].statment)
//          for(int i=0;i<words.length();i++){
//            $('#word').text(pages[pageNum].words[i].)
//        }
        
        $('#word').text(pages[pageNum].words["0"].name0)
        $('#word_1').text(pages[pageNum].words["1"].name1)
        $('#word_2').text(pages[pageNum].words["2"].name2)
        $('#questionTitle').text(pages[pageNum].title);

        

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
    $(afterLocation).after('<div id="main_content"></div><div class="clearfix"></div><div class="col-md-4 col-md-offset-8 col-sm-4 col-sm-offset-4 col-xs-12"><button id="btn-sumit" type="button" class="btn btn-primary btn-lg btn-block">تحقق الأن</button><br/></div><!--end col-xs-12-->')
     
     $("#main_container").append(' <div class="row"><div class="col-xs-12"><nav class="store_paging"><ul class="pagination"></ul></nav></div></div><!--end row-->');
     
}
function paging(id)
{
    start(id);
    
    
}
function getNext()
{
    var id =Number($(".active").attr("id"))+1;
    if(id>count)
        return false;
    start(id);
    
}

function getprev()
{
    var id =Number($(".active").attr("id"))-1;
    if(id<0)
        return false;
   start(id);
}

