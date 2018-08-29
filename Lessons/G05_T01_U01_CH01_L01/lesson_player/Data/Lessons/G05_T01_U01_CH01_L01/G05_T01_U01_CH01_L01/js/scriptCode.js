
var levelnumber = 1;
var sec;
var score=0;
var audioElement = document.createElement('audio');//to play sound
jQuery(document).ready(function(e) {
    $('*').addClass('unselectable');
    
    $('#quizz').click(function(){
        $('.game_info').fadeIn(1000);
        $('body').css('overflow','hidden');
        $('.refresh_page').css('visibility','hidden');
    });
    $('#myrefresh').click(function(){
        $('.total_score').fadeIn(1000);
    });
    $('.exit_info').click(function(){
        $('.game_info').css('display','none');
        $('body').css('overflow','auto');
        $('.refresh_page').css('display','block');
    });
    $('.next_btn').click(function(){
        $('.total_score').css('display','none');
    });  
    
    $('.top_title').prepend(sourceData[0].header_arb);
//    $('.sp1').html(sourceData[0].first_num);
    $('.question_name').html(sourceData[0].question_tag);
    
    loadQuizData()
	new WOW().init();
});

function scrolltop(){
   $("html, body").animate({ scrollTop: 0 }, "fast");
   return false;
}
alltime=0;
var alltime2=0;
function loadQuizData()
{
    alltime2+=alltime;    
    $('.fa-check').css('display','none');
   $('.fa-times').css('display','none');
    $('.pressure_load').height('0px');
    $('.ballon_ques').html(sourceData[levelnumber].headQuiz);
    for(var num=1;num<=6;num++)
    {
        $('#option'+num).empty();
        $('#option'+num).append(sourceData[levelnumber]['option'+num]);
    }
  
}

//count time of game
var ss='';
var sec = 16;   // set the seconds
var remain;
var alltime;
function countDown() {
    cc=16-sec;
   sec--;
  if (sec == -01) {
   sec = 00;
   }  
if (sec<=9) { sec = "0" + sec; }

  time = "00:"+sec;
    remain="00:"+(16-sec);
    if ((16-sec)<=9){remain="0"+(16-sec)+ " :00 ";
                }
if (document.getElementById) {
    ss=remain;
                              document.getElementById('remaintime').innerHTML =time;
                             }
alltime=16-sec;
SD=window.setTimeout("countDown();", 1000);
    if (sec == '00') {
        sec = "00"; 
        window.clearTimeout(SD);
        $('.time_over_msg').text('لقد انتهى الوقت المحدد لإنهاء المرحلة.');
        $('.time_over').show(); 
        $('.refresh').css('visibility','hidden');
        
        scrolltop();
        setTimeout(function(){
            goToNextLevel();
            $('.time_over').hide();
            $('.refresh').css('visibility','visible');
        },2000);

    }
}
window.onload = countDown;
//when user clicks on any tube
function answer(tubeID)
{ 
    //alert(tubeID)
    if(tubeID==sourceData[levelnumber].correctOption)
    {
        var quiz_nums=sourceData.length-1;
        score++;
       window.clearTimeout(SD);
          $('<audio id="audioDemo" controls preload="auto"><source src="sound/correctAnswer.mp3" type="audio/mp3"><source src="sound/correctAnswer.ogg" type="audio/ogg"></audio>')[0].play()
        $('.fa-check').css('display','block');
        $('.fa-times').css('display','none');
        $('.next_btn').text('إجمع أكثر');
     $('.next_btn').attr("href","javascript:goToNextLevel();");
        
        for(var xx=1;xx<=score;xx++)
        {
            if($('#span'+xx).hasClass('colored')==false)
            {
                $('#span'+xx).addClass('colored');
            }
        }
        if(levelnumber!=quiz_nums){
            setTimeout(function(){goToNextLevel();},1000);
//            $('.total_score').delay(1000).fadeIn(500);
        }
        else if(levelnumber==quiz_nums)
        {
            clearTimeout(SD);
            setTimeout(function(){
                testresult();
                $('.myresult').css('display','block');
                $('.refresh').css('visibility','hidden');
                $('.fa-check').css('display','none');
                $('.fa-times').css('display','none');
                scrolltop();
                $('body').css('overflow','hidden');
            },1000);                      
        }
    }
    else{
        $('.fa-times').css('display','block');
        $('.fa-check').css('display','none');
         window.clearTimeout(SD);
        $('<audio id="audioDemo" controls preload="auto"><source src="sound/wrongAnswer.mp3" type="audio/mp3"><source src="sound/wrongAnswer.ogg" type="audio/ogg"></audio>')[0].play()

        $('.next_btn').text('إجمع أكثر');
        $('.next_btn').attr("href","javascript:goToNextLevel();");
        var quiz_nums=sourceData.length-1;
        if(levelnumber!=quiz_nums){
            setTimeout(function(){goToNextLevel();},1000);
//         $('.total_score').delay(1000).fadeIn(500);            
        }
        else if(levelnumber==quiz_nums){
            clearTimeout(SD)
            setTimeout(function(){
                testresult();
                $('.myresult').css('display','block'); 
                $('.refresh').css('visibility','hidden');
                $('.fa-check').css('display','none');
                $('.fa-times').css('display','none');
                scrolltop();
                $('body').css('overflow','hidden');
            },1000);
        }
    }
}

//go to next level when user click on next button
function goToNextLevel()
{
    levelnumber++;
    $('#nextButton').empty();        
    $('.total_score').css('display','none');
    loadQuizData();
     sec = 16; 
           countDown();
}

function showScore()
{
    $('.next_btn').text('أكمل اللعبة');
    $('.next_btn').attr("href","javascript:$('.total_score').css('display','none');countDown();");
    //document.getElementsByClassName('next_btn
    $('.total_score').fadeIn(500);
}
function testresult(){
    var quiz_nums=sourceData.length-1;
    $('.result_rate').html('('+quiz_nums+'/'+score+')');
    var score_res=score*5/quiz_nums;    
    score_res=""+score_res+"";
    if(score_res.length != 1){
        score_res=score_res.charAt(0)+score_res.charAt(1)+score_res.charAt(2);
    }
    
    
    if(score_res==0 || score_res==0.1 || score_res==0.2 || score_res==0.3 || score_res==0.4 || score_res==0.5){
        $('#star1').addClass('win_half');
    }
    else if(score_res==0.6 || score_res==0.7 || score_res==0.8 || score_res==0.9 ||score_res==1){
        $('#star1').addClass('win');        
    }
    else if(score_res==1.1 || score_res==1.2 || score_res==1.3 || score_res==1.4 || score_res==1.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win_half');        
    }
    else if(score_res==1.6 || score_res==1.7 || score_res==1.8 || score_res==1.9 || score_res==2){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
    }
    else if(score_res==2.1 || score_res==2.2 || score_res==2.3 || score_res==2.4 || score_res==2.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win_half');
    }
    else if(score_res==2.6 || score_res==2.7 || score_res==2.8 || score_res==2.9 || score_res==3){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
    }
    else if(score_res==3.1 || score_res==3.2 || score_res==3.3 || score_res==3.4 || score_res==3.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win_half');        
    }
    else if(score_res==3.6 || score_res==3.7 || score_res==3.8 || score_res==3.9 || score_res==4){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win');
    }
    else if(score_res==4.1 || score_res==4.2 || score_res==4.3 || score_res==4.4 || score_res==4.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win');
        $('#star5').addClass('win_half');        
    }
    else if(score_res==4.6 || score_res==4.7 || score_res==4.8 || score_res==4.9 || score_res==5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win');
        $('#star5').addClass('win');
    }
    $('.established_time').html('');
    $('body').css('overflow','hidden');
    if(alltime2<10){
        $('.established_time').append('(ثانية 00:0'+alltime2+' دقيقة)');        
    }else if(alltime2>=10 && alltime2<60){
        $('.established_time').append('(ثانية 00:'+alltime2+' دقيقة)');
    }else if(alltime2>=60){
        var x=parseInt(alltime2/60);
        var x2=alltime2%60;
        if(x2<10){
            $('.established_time').append('(ثانية 0'+x+':0'+x2+' دقيقة)');
        }else if(x2>=10){
            $('.established_time').append('(ثانية 0'+x+':'+x2+' دقيقة)');
        }        
    }    
}