function [mdl_area,mdl_height,mdl_merge] = post_proc1(mz,y,mdl,opts)

if opts.if_merge
    %merging
    mdl_merge = components_merging(mdl,opts.mz_thr,finv(1 - .05,2,2),0);
    while 1
       tmp = length(mdl_merge.w);
       mdl_merge = components_merging(mdl,opts.mz_thr,finv(1 - .05,2,2),0);
       if length(mdl_merge.w) == tmp; break; end
    end
%     if opts.draw
%         figure; plot_gmm(mz,y,mdl_merge.w,mdl_merge.mu,mdl_merge.sig);
%         hold on; plot_gmm(mz,y,mdl.w,mdl.mu,mdl.sig);
%         title([num2str(mdl_merge.KS) ' peaks left.' num2str(mdl.KS-mdl_merge.KS) ' merged.'])
%     end
    mdl = mdl_merge;
else
    mdl_merge = mdl;
end

if opts.if_rem
    % feature reduction
    area = zeros(1,mdl.KS); height = area;
    for a=1:mdl.KS
        y_tmp = mdl.w(a)*normpdf(mz,mdl.mu(a),mdl.sig(a));
        area(a) = trapz(mz,y_tmp);
        height(a) = max(y_tmp);
    end

    % figure; subplot(2,1,1); hist(area,sqrt(mdl.KS)); subplot(2,1,2); hist(log(area),sqrt(mdl.KS));
    % figure; subplot(2,1,1); hist(height,sqrt(mdl.KS)); subplot(2,1,2); hist(log(height),sqrt(mdl.KS));

    % outlier detection
    [~,~,idxl_a,idx_a] = Bruffaerts_out(area);
    if sum(idx_a) > opts.thr
        mdl_area = del_gmm(mdl,~idx_a);
        thr_a = max(area(~idx_a));
    else
        mdl_area = del_gmm(mdl,idxl_a);
        thr_a = max(area(idxl_a));
    end

    [~,~,idxl_h,idx_h] = Bruffaerts_out(height);
    if sum(idx_h) > opts.thr
        mdl_height = del_gmm(mdl,~idx_h);
        thr_h = max(height(~idx_h));
    else
        mdl_height = del_gmm(mdl,idxl_h);
        thr_h = max(height(idxl_h));
    end

    if opts.draw
%         figure; subplot(2,1,1)
%         if sum(idx_a) > opts.thr
%             hist(area,sqrt(mdl.KS)); xlabel('Area under the peak'); ylabel('Counts')
%             if ~isempty(thr_a); hold on; plot([thr_a,thr_a],ylim,'r'); end
%         else
%             hist(log(area),sqrt(mdl.KS)); xlabel('Log(Area under the peak)'); ylabel('Counts')
%             if ~isempty(thr_a); hold on; plot([log(thr_a),log(thr_a)],ylim,'r'); end
%         end
%         title([num2str(mdl_area.KS) ' peaks left.' num2str(mdl.KS-mdl_area.KS) ' removed.'])
%         subplot(2,1,2); plot_gmm(mz,y,mdl_area.w,mdl_area.mu,mdl_area.sig);

        figure; subplot(2,1,1)
        if sum(idx_h) > Inf
            hist(height,sqrt(mdl.KS)); xlabel('Peak height'); ylabel('Counts')
            if ~isempty(thr_h); hold on; plot([thr_h,thr_h],ylim,'r'); end
        else
            hist(log(height),sqrt(mdl.KS)); xlabel('Log(Peak height)'); ylabel('Counts')
            if ~isempty(thr_h); hold on; plot([log(thr_h),log(thr_h)],ylim,'r'); end
        end
        title([num2str(mdl_height.KS) ' peaks left.' num2str(mdl.KS-mdl_height.KS) ' removed.'])
        subplot(2,1,2); plot_gmm(mz,y,mdl_height.w,mdl_height.mu,mdl_height.sig);
    end
else
    mdl_area = mdl;
    mdl_height = mdl;
end

function mdl = del_gmm(mdl,ind)
mdl.w = mdl.w(~ind);
mdl.mu = mdl.mu(~ind);
mdl.sig = mdl.sig(~ind);
mdl.KS = length(mdl.w);
