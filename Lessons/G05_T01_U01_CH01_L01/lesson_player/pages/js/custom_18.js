var correctCards = 0;
var cardsCount,DG;
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
 /*       loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
      //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init(pageNum);
        };
  //  }
//end key code 
    
    
    }
 


function init(pageNum) {
    
  //  console.log(pages[pageNum].data_gallary[1].img_src);
    loadHTMLTags(pageNum);
  // Reset the game
  correctCards = 0;
  $('.circle_num ul').html( '' );
  $('.section_items').html( '' );
 
  // Create the pile of shuffled cards
  var numbers = [ ];
     var cardsSrc=[];
//console.log(pages[pageNum].data_gallary[0].co_answer)
    // define dg as data array for gallary from json 
    DG=pages[pageNum].data_gallary;
    cardsCount=DG.length;
//    localStorage.setItem("cardsCount", DG.length);
    //console.log(DG.length)
    for (var i=0;i<DG.length;i++){
        numbers.push({x:i+1, y:DG[i].co_answer});
        cardsSrc.push({x:i+1, y:DG[i].img_src ,z:DG[i].desc});
    }  
		
  numbers.sort( function() { return Math.random() - .5 } );
 cardsSrc.sort( function() { return Math.random() - .5 } );
  for ( var i=0; i<numbers.length; i++ ) {
      
    $('<li><div class="list_num">' + numbers[i].y + '</div></li>').data( 'number', numbers[i].x ).appendTo( '.circle_num ul' ).draggable( {
      //containment: '.cards_content',
         start:function(){ 
                    playAudio(dragSoundPath)
            },
      stack: '.circle_num li',
      cursor: 'move',
      revert: true
    } );
  }  
   
      
                                
  // Create the card slots
	
  for ( var i=0; i<DG.length; i++ ) {
    $('<div  class="grid-md-5 grid-sm-5 grid-xs-5 "><div class="sec_item"><div class="sec_img"><img src="' + cardsSrc[i].y + '" alt=""/></div><div class="sec_num"></div><p class="sec_desc">' + cardsSrc[i].z  + '</p></div></div>').data( 'number', cardsSrc[i].x ).appendTo( '.section_items' ).droppable( {
      accept: '.circle_num li',
      hoverClass: 'hovered',
      drop: handleCardDrop
    } );
  }
 
}
 //====================================
function handleCardDrop( event, ui ) {
  var slotNumber = $(this).data( 'number' );
  var cardNumber = ui.draggable.data( 'number' );
  // If the card was dropped to the correct slot,
  // change the card colour, position it directly
  // on top of the slot, and prevent it being dragged
  // again
 
  if ( slotNumber == cardNumber ) {
    ui.draggable.addClass( 'correct' );
    ui.draggable.draggable( 'disable' );
    $(this).droppable( 'disable' );
    ui.draggable.position( { of: $(this).children('.sec_item').children('.sec_num'), my: 'left top', at: 'left top' } );
    ui.draggable.draggable( 'option', 'revert', false );
    correctCards++;
    // console.log(cardNumber)
     if(DG[cardNumber-1].sub_narration!=undefined){
               //subort sound
               if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause()}
//               playTapAudio(DG[cardNumber-1].sub_narration)
           }
                     playAudio(dropSoundPath)

  } 
   
  // If all the cards have been placed correctly then display a message
  // and reset the cards for another go
 
  if ( correctCards == cardsCount ) {
   $('#card_modal').modal('show');
      playAudio (cAnswerSoundPath);
  }
 
}
function loadHTMLTags(pageNum) {
    $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    setMainContent('#main_container'); 
    $(".contents").addClass('temp_18')
    if(pages[pageNum].article!=undefined){
        setParagraph(".contents")
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);}else{$('.page_title').remove()}
        $('.page_desc').html(pages[pageNum].article.text);
        setCards(".page_desc");
        //console.log(pages[pageNum].article.text.length)
    }else{
         setCards(".contents");
    }
    setCorDeclar();
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration)}    
    }   
}
function setCards(location) {
    $(location).after('<div class="circle_num"><ul></ul></div><!--end circle_num--><div class="clearfix"></div><div class="section_items"></div><!--end section_item-->')
  
     
}

function setCorDeclar(){
    $('.container').after('<!--=========Modal=========--><div class="modal fade bs-example-modal-sm" id="card_modal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h4 class="modal-title" id="myModalLabel">تأكيد</h4></div><div class="modal-body"><div class="alert alert-success" style="text-align:center;"> أحسنت إجابة صحيحة </br> تمت جميع الإختيارات بنجاح.   </div></div></div></div></div><!--/////////////////////////////-->')
    
}