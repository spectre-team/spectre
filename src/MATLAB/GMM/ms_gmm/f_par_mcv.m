% auxiliary function for find_split_peaks
function fst_new = f_par_mcv( Kini,fst,P,Par_mcv )
%
% Kini - initial position of the peak counter
% fst - number of forward steps intended to make 
%
   if Kini+fst>size(P,1)
      fst_new=0;
   else
      fst_new=fst;
      while 1
          if P(Kini+fst_new,1)-P(Kini,1) > fst*Par_mcv*P(Kini,1)
              break
          else
              fst_new=fst_new+1;
              if Kini+fst_new>size(P,1)
                  fst_new=0;
                  break
              end
          end
      end
   end
return



